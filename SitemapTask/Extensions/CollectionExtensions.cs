using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitemapTask.Extensions
{
	public static class CollectionExtensions
	{
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
		{
			return new HashSet<T>(source);
		}
	}
}