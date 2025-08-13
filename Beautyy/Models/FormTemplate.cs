using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormTemplate")]
public partial class FormTemplate
{
    [Key]
    public int Id { get; set; }

    public string? Topic { get; set; }

    public string? ButtonName { get; set; }

    public string? Url { get; set; }

    [InverseProperty("Form")]
    public virtual ICollection<FormComponentTemplate> FormComponentTemplates { get; set; } = new List<FormComponentTemplate>();

    [InverseProperty("FormTemplate")]
    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();

    [ForeignKey("Id")]
    [InverseProperty("FormTemplate")]
    public virtual Component IdNavigation { get; set; } = null!;
}
