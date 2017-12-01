using SitemapTask.Core;
using SitemapTask.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapTask.Services.Abstract
{
	internal interface ISitemapService
	{
		IParser<IEnumerable<string>> Parser { get; set; }

		IParserSettings Settings { get; set; }

		HashSet<string> Urls { get; }

		Task<HashSet<string>> ParseUrlsAsync();

		Task<Dictionary<string, List<double>>> MeasureResponseTimeAsync();
	}
}
