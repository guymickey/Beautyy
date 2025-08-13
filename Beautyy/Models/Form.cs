using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Form")]
public partial class Form
{
    [Key]
    public int Id { get; set; }

    [Column("FormTemplateID")]
    public int? FormTemplateId { get; set; }

    public bool IsDelete { get; set; }

    public string? Topic { get; set; }

    public string? ButtonName { get; set; }

    public string? Url { get; set; }

    [InverseProperty("Form")]
    public virtual ICollection<FormComponent> FormComponents { get; set; } = new List<FormComponent>();

    [ForeignKey("FormTemplateId")]
    [InverseProperty("Forms")]
    public virtual FormTemplate? FormTemplate { get; set; }
}
