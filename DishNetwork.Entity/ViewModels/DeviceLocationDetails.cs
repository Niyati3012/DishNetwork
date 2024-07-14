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
		public bool IsError { get; set; }
		public string PropertyName { get; set; }
		public string PersonName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }

	}
}
