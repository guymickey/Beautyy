using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("EventCategory")]
public partial class EventCategory
{
    [Key]
    public int Id { get; set; }

    public int? EventId { get; set; }

    public int? CategoryId { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("EventCategories")]
    public virtual Category? Category { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("EventCategories")]
    public virtual Event? Event { get; set; }
}
