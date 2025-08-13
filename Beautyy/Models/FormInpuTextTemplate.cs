using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormInpuTextTemplate")]
public partial class FormInpuTextTemplate
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("FormInpuTextTemplate")]
    public virtual FormElementTemplate IdNavigation { get; set; } = null!;
}
