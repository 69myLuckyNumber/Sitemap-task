using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SitemapTask.Models
{
	public class HomeViewModel
	{
		public UrlModel UrlModel { get; set; }
		
		public IEnumerable<HistoryCardViewModel> HistoryCards { get; set; }
	}

	public class UrlModel
	{
		[Required(ErrorMessage = "This field is required.")]
		[RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", 
			ErrorMessage = "Invalid url.")]
		[DisplayName("Enter url to build a sitemap")]
		public string Url { get; set; }
	}
	public class HistoryCardViewModel
	{
		public int Id { get; set; }

		public string Hostname { get; set; }

		public DateTime Date { get; set; }
	}
}