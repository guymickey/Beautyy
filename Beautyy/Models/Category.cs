using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Category")]
public partial class Category
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
}
