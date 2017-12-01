using Domain.Abstract;
using Domain.Entities;
using SitemapTask.Core.Concrete;
using SitemapTask.Models;
using SitemapTask.Services.Abstract;
using SitemapTask.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SitemapTask.Controllers
{
    public class ParserController : Controller
    {
		private IRepository<Host> _repository;
		public ParserController(IRepository<Host> hostRepo)
		{
			_repository = hostRepo;
		}
        [HttpGet]
        public ActionResult Index()
        {
			var hosts = _repository.Entities;
			var histories = new List<HistoryCardViewModel>();
			Parallel.ForEach(hosts, host =>
			{
				histories.Add(new HistoryCardViewModel
				{
					Id = host.HostId,
					Date = host.TimeCreated,
					Hostname = host.HostUrl
				});
			});
			return View(new HomeViewModel { UrlModel = new UrlModel(), HistoryCards = histories });
        }

		[HttpGet]
		public ActionResult History(int id = 1)
		{
			var host = _repository.GetById(id);
			if (host == null)
				throw new HttpException(404, "Not found");

			var dict = new Dictionary<string, List<double>>();
			var locker = new object();
			Parallel.ForEach(host.Pages, page => 
			{
				lock (locker)
				{
					dict.Add(page.Url, new List<double> { page.MinResponseTime, page.MaxResponseTime });
				}		
			});
			return View(new HistoryViewModel
			{
				Data = dict.OrderByDescending(i=>i.Value.Average()).ToDictionary(p=>p.Key, p=>p.Value),
				Date = host.TimeCreated,
				Hostname = host.HostUrl
			});
		}
		[HttpPost]
		public async Task<ActionResult> Index(UrlModel model)
		{
			if (ModelState.IsValid)
			{
				var uri = new Uri(model.Url);
				string baseUrl = $"{uri.Scheme}://{uri.Authority}";

				ISitemapService sitemapService = new SitemapService(
					new UrlParser(),
					new UrlParserSettings(baseUrl)
					);
				var urls = await sitemapService.MeasureResponseTimeAsync();

				var host = new Host
				{
					HostUrl = baseUrl,
					TimeCreated = DateTime.Now,
					Pages = new List<Page>()
				};
				var locker = new object();
				Parallel.ForEach(urls, u =>
				{
					lock (locker)
					{
						host.Pages.Add(new Page
						{
							Host = host,
							MaxResponseTime = u.Value.Max(),
							MinResponseTime = u.Value.Min(),
							Url = u.Key
						});
					}

				});
				_repository.Add(host);
				_repository.Commit();
				return RedirectToAction("History", new { id = host.HostId });
			}
			return PartialView("_UrlView", model);
		}
	}
}
