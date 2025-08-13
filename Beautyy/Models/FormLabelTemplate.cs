using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormLabelTemplate")]
public partial class FormLabelTemplate
{
    [Key]
    public int Id { get; set; }

    public string? LabelText { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("FormLabelTemplate")]
    public virtual FormElementTemplate IdNavigation { get; set; } = null!;
}
