using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

[Table("UserWiseMenu")]
public partial class UserWiseMenu
{
    [Key]
    public int UserWiseMenuId { get; set; }

    public int UserId { get; set; }

    public int MenuId { get; set; }

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

    [ForeignKey("CreatedBy")]
    [InverseProperty("UserWiseMenus")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [ForeignKey("MenuId")]
    [InverseProperty("UserWiseMenus")]
    public virtual Menu Menu { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserWiseMenus")]
    public virtual User User { get; set; } = null!;
}
