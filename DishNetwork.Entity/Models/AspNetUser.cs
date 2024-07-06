using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

[Table("AspNetUser")]
public partial class AspNetUser
{
    [Key]
    [Column(TypeName = "character varying")]
    public string AspNetUserId { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string EmailId { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string PassWord { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? Ex1 { get; set; }

    [Column(TypeName = "character varying")]
    public string? Ex2 { get; set; }

    [Column(TypeName = "character varying")]
    public string? Ex3 { get; set; }

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    [Column(TypeName = "character varying")]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column(TypeName = "character varying")]
    public string? ModifiedBy { get; set; }

    [InverseProperty("AspNetUser")]
    public virtual ICollection<Admin> AdminAspNetUsers { get; set; } = new List<Admin>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Admin> AdminCreatedByNavigations { get; set; } = new List<Admin>();

    [InverseProperty("AspNetUser")]
    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    [InverseProperty("AspNetUser")]
    public virtual ICollection<Reseller> ResellerAspNetUsers { get; set; } = new List<Reseller>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Reseller> ResellerCreatedByNavigations { get; set; } = new List<Reseller>();

    [InverseProperty("AspNetUser")]
    public virtual ICollection<User> UserAspNetUsers { get; set; } = new List<User>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<User> UserCreatedByNavigations { get; set; } = new List<User>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<UserWiseMenu> UserWiseMenus { get; set; } = new List<UserWiseMenu>();
}
