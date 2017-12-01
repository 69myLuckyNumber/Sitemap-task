using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitemapTask.Models
{
	public class HistoryViewModel
	{
		public string Hostname { get; set; }

		public DateTime Date { get; set; }

		public Dictionary<string, List<double>> Data { get; set; }
	}
}