using SitemapTask.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitemapTask.Core.Concrete
{
	public class UrlParserSettings : IParserSettings
	{
		public UrlParserSettings(string url)
		{
			BaseUrl = url;

		}
		public string BaseUrl { get; set; }
	}
}