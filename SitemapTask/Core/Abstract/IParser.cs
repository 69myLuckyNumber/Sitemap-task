
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapTask.Core.Abstract
{
	public interface IParser<T> where T : class
	{
		T Parse(IDocument document, IParserSettings settings);
	}
}
