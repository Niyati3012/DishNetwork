using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Entity.ViewModels
{
	public class DeviceLocationDetails
	{
		public int DeviceId { get; set; }
		public string IPAddress { get; set; }
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set;}

	}
}
