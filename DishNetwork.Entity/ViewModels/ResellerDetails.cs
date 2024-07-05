using DishNetwork.Entity.Models;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http;


namespace DishNetwork.Entity.ViewModels
{
    public class ResellerDetails
    {
        public int ResellerId { get; set; }

        public string AspNetUserId { get; set; }

        public string Name { get; set; }

        public string EmailId { get; set; }

        public string? ContactNumber { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? ZipCode { get; set; }

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
