using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

[Table("FileLog")]
public partial class FileLog
{
    [Key]
    public int FileLogId { get; set; }

    public int DeviceId { get; set; }

    [Column(TypeName = "character varying")]
    public string? FirstFile { get; set; }

    [Column(TypeName = "character varying")]
    public string? SecondFile { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DeletedDate { get; set; }

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

    [ForeignKey("DeviceId")]
    [InverseProperty("FileLogs")]
    public virtual Device Device { get; set; } = null!;
}
