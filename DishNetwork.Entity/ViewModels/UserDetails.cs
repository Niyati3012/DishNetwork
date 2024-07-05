using DishNetwork.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Entity.ViewModels
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public int ReSellerId { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; } 
        public string? ContactNumber { get; set; }
        public string AspNetUserId { get; set; }
        public string? Description { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Menus {  get; set; }
        public List<string> MenuNames { get; set; }
        public List<UserDetails> Users { get; set; }
    }
}
