using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

[Table("Reseller")]
public partial class Reseller
{
    [Key]
    public int ResellerId { get; set; }

    [Column(TypeName = "character varying")]
    public string AspNetUserId { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    public string EmailId { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? ContactNumber { get; set; }

    [Column(TypeName = "character varying")]
    public string? State { get; set; }

    [Column(TypeName = "character varying")]
    public string? City { get; set; }

    [Column(TypeName = "character varying")]
    public string? Address1 { get; set; }

    [Column(TypeName = "character varying")]
    public string? Address2 { get; set; }

    [Column(TypeName = "character varying")]
    public string? ZipCode { get; set; }

    [Column(TypeName = "character varying")]
    public string? Logo { get; set; }

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

    [Column(TypeName = "character varying")]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("AspNetUserId")]
    [InverseProperty("ResellerAspNetUsers")]
    public virtual AspNetUser AspNetUser { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("ResellerCreatedByNavigations")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [InverseProperty("ReSeller")]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    [InverseProperty("ReSeller")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
