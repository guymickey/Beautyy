using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("ComponentElement")]
public partial class ComponentElement
{
    [Key]
    public int Id { get; set; }

    public string? Element { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual ButtonElement? ButtonElement { get; set; }

    [InverseProperty("ComponentElement")]
    public virtual ICollection<CombineElement> CombineElements { get; set; } = new List<CombineElement>();

    [InverseProperty("IdNavigation")]
    public virtual DateT? DateT { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Number? Number { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Picture? Picture { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Text? Text { get; set; }
}
