using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Text")]
public partial class Text
{
    [Key]
    public int Id { get; set; }

    public string? TextValue { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Text")]
    public virtual ComponentElement IdNavigation { get; set; } = null!;
}
