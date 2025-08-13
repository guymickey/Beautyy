using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormInputFileTemplate")]
public partial class FormInputFileTemplate
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("FormInputFileTemplate")]
    public virtual FormElementTemplate IdNavigation { get; set; } = null!;
}
