using DishNetwork.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Entity.ViewModels
{
    public class DevicesDetails
    {
        public int DeviceId { get; set; }
        public int ReSellerId { get; set; }
        public string Ipaddress { get; set; } 
        public string Port { get; set; }
        public string? PersonName { get; set; }
        public string? PropertyName { get; set; }
        public string? NoOfRooms { get; set; }
        public short? Equipment { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool MaintainanceContract { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? GoogleMap { get; set; }
        public string? EmailId { get; set; }
        public string? ContactNo { get; set; }
        public string? VpnUserName { get; set; }
        public string? VpnPassword { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
