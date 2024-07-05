using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

[Table("Menu")]
public partial class Menu
{
    [Key]
    public int MenuId { get; set; }

    [Column(TypeName = "character varying")]
    public string MenuName { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    public int Sequence { get; set; }

    [Column(TypeName = "character varying")]
    public string? Icon { get; set; }

    [InverseProperty("Menu")]
    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();

    [InverseProperty("Menu")]
    public virtual ICollection<UserWiseMenu> UserWiseMenus { get; set; } = new List<UserWiseMenu>();
}
