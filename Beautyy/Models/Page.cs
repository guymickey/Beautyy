using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Page")]
public partial class Page
{
    [Key]
    public int Id { get; set; }

    public int? EventId { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Pages")]
    public virtual Event? Event { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Page")]
    public virtual Container IdNavigation { get; set; } = null!;
}
