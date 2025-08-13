using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Event")]
public partial class Event
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsFav { get; set; }

    public int? ImageId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();

    [ForeignKey("ImageId")]
    [InverseProperty("Events")]
    public virtual ImageFile? Image { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();
}
