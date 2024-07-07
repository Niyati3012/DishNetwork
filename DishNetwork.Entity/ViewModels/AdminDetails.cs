using DishNetwork.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Entity.ViewModels
{
    public class AdminDetails
    {
        public int AdminId { get; set; }
        public string AspNetUserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } 
        public string Password { get; set; }
        [Required(ErrorMessage = "EmailId is required")]
        public string EmailId { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<Admin> adminlist { get; set; }
    }
}
