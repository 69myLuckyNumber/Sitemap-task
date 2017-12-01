using SitemapTask.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngleSharp.Dom.Html;
using SitemapTask.Extensions;
using AngleSharp.Dom;

namespace SitemapTask.Core.Concrete
{
	public class UrlParser : IParser<IEnumerable<string>>
	{
		public IEnumerable<string> Parse(IDocument document, IParserSettings settings)
		{
			var items = document.QuerySelectorAll("a")
				.OfType<IHtmlAnchorElement>()
				.Where((IHtmlAnchorElement item) =>
				{
					string href = settings.BaseUrl + item.PathName;
					if (Uri.IsWellFormedUriString(href, UriKind.Absolute)
					&& item.PathName.Contains("/")
					&& (item.HostName.Equals(String.Empty) || item.Href.Contains(settings.BaseUrl))
					&& !item.PathName.Equals(String.Empty))
					{
						return true;
					}
					return false;
				})
				.Select((IHtmlAnchorElement item) => settings.BaseUrl + item.PathName);
			return items;
		}
	}
}