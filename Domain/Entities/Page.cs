using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Page
	{
		[Key]
		public int PageId { get; set; }

		public int HostId { get; set; }
		public virtual Host Host { get; set; }

		public string Url { get; set; }

		public double MinResponseTime { get; set; }
		public double MaxResponseTime { get; set; }
	}
}
