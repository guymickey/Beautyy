using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormComponent")]
public partial class FormComponent
{
    [Key]
    public int Id { get; set; }

    public int? FormId { get; set; }

    [Column("FormComponentTemplateID")]
    public int? FormComponentTemplateId { get; set; }

    public string? TypeForm { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("FormId")]
    [InverseProperty("FormComponents")]
    public virtual Form? Form { get; set; }

    [InverseProperty("FormCompoent")]
    public virtual ICollection<FormCombineElement> FormCombineElements { get; set; } = new List<FormCombineElement>();

    [ForeignKey("FormComponentTemplateId")]
    [InverseProperty("FormComponents")]
    public virtual FormComponentTemplate? FormComponentTemplate { get; set; }
}
