using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
	public class LocationRoi
	{
		public int Id { get; set; }
		public string Location { get; set; }
		public decimal RoiPercentage { get; set; }
		public ICollection<Unit>? Unit { get; set; }
	}
}
