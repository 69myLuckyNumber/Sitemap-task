using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Host
	{
		[Key]
		public int HostId { get; set; }

		public string HostUrl { get; set; }

		public virtual DateTime TimeCreated { get; set; }

		public virtual ICollection<Page> Pages { get; set; }

		
	}
}
