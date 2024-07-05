using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

public partial class AspNetRole
{
    [Key]
    public int AspNetRoleId { get; set; }

    [Column(TypeName = "character varying")]
    public string RoleName { get; set; } = null!;

    [InverseProperty("AspNetRole")]
    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();
}
