using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.Models;

[Table("Device")]
public partial class Device
{
    [Key]
    public int DeviceId { get; set; }

    public int ReSellerId { get; set; }

    [Column("IPAddress")]
    [StringLength(256)]
    public string Ipaddress { get; set; } = null!;

    [StringLength(128)]
    public string Port { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? PersonName { get; set; }

    [Column(TypeName = "character varying")]
    public string? PropertyName { get; set; }

    [Column(TypeName = "character varying")]
    public string? NoOfRooms { get; set; }

    public short? Equipment { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ExpiryDate { get; set; }

    public bool MaintainanceContract { get; set; }

    public string? Address { get; set; }

    [Column(TypeName = "character varying")]
    public string? State { get; set; }

    [Column(TypeName = "character varying")]
    public string? City { get; set; }

    [Column(TypeName = "character varying")]
    public string? ZipCode { get; set; }

    [Column(TypeName = "character varying")]
    public string? GoogleMap { get; set; }

    [Column(TypeName = "character varying")]
    public string? EmailId { get; set; }

    [Column(TypeName = "character varying")]
    public string? ContactNo { get; set; }

    [Column("VPN UserName", TypeName = "character varying")]
    public string? VpnUserName { get; set; }

    [Column("VPN Password", TypeName = "character varying")]
    public string? VpnPassword { get; set; }

    [Column(TypeName = "character varying")]
    public string? Ex1 { get; set; }

    [Column(TypeName = "character varying")]
    public string? Ex2 { get; set; }

    [Column(TypeName = "character varying")]
    public string? Ex3 { get; set; }

    [Column(TypeName = "character varying")]
    public string? Ex4 { get; set; }

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

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Devices")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [InverseProperty("Device")]
    public virtual ICollection<FileLog> FileLogs { get; set; } = new List<FileLog>();

    [ForeignKey("ReSellerId")]
    [InverseProperty("Devices")]
    public virtual Reseller ReSeller { get; set; } = null!;
}
