using SitemapTask.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SitemapTask.Core.Abstract;
using System.Threading.Tasks;
using SitemapTask.Core;
using System.Diagnostics;
using AngleSharp.Dom;

namespace SitemapTask.Services.Concrete
{
	public class SitemapService : ISitemapService
	{
		public IParser<IEnumerable<string>> Parser { get; set; }
		public IParserSettings Settings { get; set; }

		public HashSet<string> Urls { get; private set; }
		public Dictionary<string, List<double>> Measurements { get; private set; }

		private HtmlLoader loader;

		public SitemapService(IParser<IEnumerable<string>> parser, IParserSettings settings) : this(parser)
		{
			Settings = settings;
		}
		public SitemapService(IParser<IEnumerable<string>> parser)
		{
			Parser = parser;
			Urls = new HashSet<string>();
			loader = new HtmlLoader();
			Measurements = new Dictionary<string, List<double>>();
		}

		public async Task<HashSet<string>> ParseUrlsAsync()
		{
			var lockThread = new object();
			var doc = await loader.GetSourceAsync(Settings.BaseUrl);

			var temp = new HashSet<string>();
			temp.UnionWith(Parser.Parse(doc, Settings));
			Urls.UnionWith(temp);

			int total = 1; 
			int count = Urls.Count; 

			//tasks for parallel url parsing
			var tasks = new List<Task>();
			for (int i = 0; i < total; i++)
			{
				//creates tasks for new urls to parse
				foreach (var tempUrl in temp.ToList()) 
				{
					tasks.Add(Task.Run(async () =>
					{
						doc = await loader.GetSourceAsync(tempUrl);
						// only one access at a time
						lock (lockThread)
						{
							if(doc!= null)
							temp.UnionWith(Parser.Parse(doc, Settings));
						}
					}));
				}
				await Task.WhenAll(tasks);
				tasks.Clear();
				Urls.UnionWith(temp);
				// new urls to parse on next iteration
				temp = new HashSet<string>(Urls.Skip(count).Take(Urls.Count - count)); 
				if (temp.Count > 0 && temp != null)
				{
					total++;
					count = Urls.Count;
				}
				if (Urls.Count > 125)
					return Urls;
			}
			return Urls;
		}
		public async Task<Dictionary<string, List<double>>> MeasureResponseTimeAsync()
		{
			if(Urls.Count == 0)
			{
				await ParseUrlsAsync();	
			}
			//parallel url response measuring
			List<Task> tasks = new List<Task>();
			foreach (var url in Urls)
			{
				tasks.Add(Task.Run(async ()=> 
				{
					//parallel time measuring (3 times)
					var task0 = GetResponseTimeAsync(url);
					var task1 = GetResponseTimeAsync(url);
					var task2 = GetResponseTimeAsync(url);
					Measurements.Add(url,
						new List<double>()
						{
							await task0,
							await task1,
							await task2
						});
				}));
			}
			await Task.WhenAll(tasks);
			return Measurements;
		}
		private async Task<double> GetResponseTimeAsync(string url)
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();
			var response = await loader.GetSourceAsync(url);
			if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				timer.Stop();
			}
			else
			{
				timer.Reset();
			}
			return timer.Elapsed.TotalSeconds;
		}
	}
}