using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

public partial class AspNetUserRole
{
    [Key]
    public int AspNetUserRolesId { get; set; }

    [Column(TypeName = "character varying")]
    public string AspNetUserId { get; set; } = null!;

    public int AspNetRoleId { get; set; }

    [ForeignKey("AspNetRoleId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetRole AspNetRole { get; set; } = null!;

    [ForeignKey("AspNetUserId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetUser AspNetUser { get; set; } = null!;
}
