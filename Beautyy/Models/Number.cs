using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Number")]
public partial class Number
{
    [Key]
    public int Id { get; set; }

    public double? NumberValue { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Number")]
    public virtual ComponentElement IdNavigation { get; set; } = null!;
}
