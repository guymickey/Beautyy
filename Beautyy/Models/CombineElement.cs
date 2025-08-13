using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("CombineElement")]
public partial class CombineElement
{
    [Key]
    public int Id { get; set; }

    public int? ComponentId { get; set; }

    [Column("ComponentElementID")]
    public int? ComponentElementId { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("ComponentId")]
    [InverseProperty("CombineElements")]
    public virtual Component? Component { get; set; }

    [ForeignKey("ComponentElementId")]
    [InverseProperty("CombineElements")]
    public virtual ComponentElement? ComponentElement { get; set; }
}
