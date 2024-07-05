using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

[Table("Log")]
public partial class Log
{
    [Key]
    public int LogId { get; set; }

    public int? Id { get; set; }

    [Column(TypeName = "character varying")]
    public string? Operation { get; set; }

    [Column(TypeName = "character varying")]
    public string? Type { get; set; }

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

    [ForeignKey("CreatedBy")]
    [InverseProperty("Logs")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;
}
