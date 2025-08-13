using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormComponentTemplate")]
public partial class FormComponentTemplate
{
    [Key]
    public int Id { get; set; }

    public int? FormId { get; set; }

    public string? TypeForm { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Birthdate? Birthdate { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual ButtonForm? ButtonForm { get; set; }

    [InverseProperty("FormComponent")]
    public virtual ICollection<CombineFormElementTemplate> CombineFormElementTemplates { get; set; } = new List<CombineFormElementTemplate>();

    [InverseProperty("IdNavigation")]
    public virtual Date? Date { get; set; }

    [ForeignKey("FormId")]
    [InverseProperty("FormComponentTemplates")]
    public virtual FormTemplate? Form { get; set; }

    [InverseProperty("FormComponentTemplate")]
    public virtual ICollection<FormComponent> FormComponents { get; set; } = new List<FormComponent>();

    [InverseProperty("IdNavigation")]
    public virtual ImageUploadwithImagecontent? ImageUploadwithImagecontent { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Imageupload? Imageupload { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Popup? Popup { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Singleselection? Singleselection { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Textfield? Textfield { get; set; }
}
