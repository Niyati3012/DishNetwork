using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    public int ReSellerId { get; set; }

    [Column(TypeName = "character varying")]
    public string UserName { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? ContactNumber { get; set; }

    [Column(TypeName = "character varying")]
    public string AspNetUserId { get; set; } = null!;

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

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("AspNetUserId")]
    [InverseProperty("UserAspNetUsers")]
    public virtual AspNetUser AspNetUser { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("UserCreatedByNavigations")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [ForeignKey("ReSellerId")]
    [InverseProperty("Users")]
    public virtual Reseller ReSeller { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<UserWiseMenu> UserWiseMenus { get; set; } = new List<UserWiseMenu>();
}
