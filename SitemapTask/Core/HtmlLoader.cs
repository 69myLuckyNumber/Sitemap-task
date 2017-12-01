using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SitemapTask.Core
{
	public class HtmlLoader
	{
		public IConfiguration Config { get; private set; }
		public HtmlLoader()
		{
			Config = Configuration.Default.WithDefaultLoader();
		}
		public async Task<IDocument> GetSourceAsync(string url)
		{
			var document = await BrowsingContext.New(Config).OpenAsync(url);

			return document;	
		}
	}
}