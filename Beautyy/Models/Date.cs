using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Date")]
public partial class Date
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Date")]
    public virtual FormComponentTemplate IdNavigation { get; set; } = null!;
}
