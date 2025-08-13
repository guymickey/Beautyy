using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("DateT")]
public partial class DateT
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateTimeValue { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("DateT")]
    public virtual ComponentElement IdNavigation { get; set; } = null!;
}
