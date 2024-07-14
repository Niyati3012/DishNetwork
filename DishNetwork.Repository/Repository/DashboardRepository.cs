using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;

namespace DishNetwork.Repository.Repository
{
	public class DashboardRepository : IDashboardRepository
	{
		private readonly ApplicationDbContext _context;

		public DashboardRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<DeviceLocationDetails> DeviceLocationDetails()
		{
			List<DeviceLocationDetails> details = new List<DeviceLocationDetails>();
			details = (from device in _context.Devices
					   where !device.DeletedAt.HasValue
					   select new DeviceLocationDetails
					   {
						   DeviceId = device.DeviceId,
						   IPAddress = device.Ipaddress,
						   Latitude = device.Latitude,
						   Longitude = device.Longitude,
						   //IsError = true,
						   PropertyName = device.PropertyName,
						   PersonName = device.PersonName,
						   Address = device.Address,
						   City = device.City,
						   State = device.State,

					   }).ToList();

			return details;
		}

		public FileLog? FileLog(int id)
		{
			FileLog? fileLog = new FileLog();
			fileLog = _context.FileLogs.Where(e=>e.DeviceId == id).OrderByDescending(e=>e.CreatedDate).FirstOrDefault();

			return fileLog;
		}
	}
}