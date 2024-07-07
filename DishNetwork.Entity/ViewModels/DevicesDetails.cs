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
        [Required(ErrorMessage = "IP Address is required")]
        public string Ipaddress { get; set; }
        [Required(ErrorMessage = "Port is required")]
        public string Port { get; set; }
        [Required(ErrorMessage = "Person Name is required")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "Property Name is required")]
        public string? PropertyName { get; set; }
        public string? NoOfRooms { get; set; }
        public short? Equipment { get; set; }
        public DateTime? ExpiryDate { get; set; }
        [Required(ErrorMessage = "Maintainance Contract is required")]
        public bool MaintainanceContract { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "GoogleMap is required")]
        public string GoogleMap { get; set; }
        [Required(ErrorMessage = "EmailId is required")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "ContactNo is required")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "VPN UserName is required")]
        public string VpnUserName { get; set; }
        [Required(ErrorMessage = "VPN Password is required")]
        public string VpnPassword { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
