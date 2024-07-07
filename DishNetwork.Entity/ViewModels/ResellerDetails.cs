using DishNetwork.Entity.Models;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Http;


namespace DishNetwork.Entity.ViewModels
{
    public class ResellerDetails
    {
        public int ResellerId { get; set; }

        public string AspNetUserId { get; set; }
        [Required(ErrorMessage = "Name is requierd")]
        public string Name { get; set; }
		[Required(ErrorMessage = "EmailId is requierd")]
		public string EmailId { get; set; }
		[Required(ErrorMessage = "Contact Number is requierd")]
		public string? ContactNumber { get; set; }
		[Required(ErrorMessage = "State is requierd")]
		public string? State { get; set; }
		[Required(ErrorMessage = "City is requierd")]
		public string? City { get; set; }
		[Required(ErrorMessage = "Address1 is requierd")]
		public string? Address1 { get; set; }
		[Required(ErrorMessage = "Address2 is requierd")]
		public string? Address2 { get; set; }
		[Required(ErrorMessage = "ZipCode is requierd")]
		public string? ZipCode { get; set; }
		[Required(ErrorMessage = "Logo is requierd")]
		public string? Logo { get; set; }
        public IFormFile Logoimage {  get; set; } 

        public string CreatedBy { get; set; } 

        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedAt { get; set; }

        public List<Reseller> Resellers { get; set; }
        
    }
}
