using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormElementTemplate")]
public partial class FormElementTemplate
{
    [Key]
    public int Id { get; set; }

    public string? ElementForm { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual ButtonElementForm? ButtonElementForm { get; set; }

    [InverseProperty("FormElement")]
    public virtual ICollection<CombineFormElementTemplate> CombineFormElementTemplates { get; set; } = new List<CombineFormElementTemplate>();

    [InverseProperty("FormElementTeplate")]
    public virtual ICollection<FormElement> FormElements { get; set; } = new List<FormElement>();

    [InverseProperty("IdNavigation")]
    public virtual FormInpuTextTemplate? FormInpuTextTemplate { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual FormInputDateTemplate? FormInputDateTemplate { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual FormInputFileTemplate? FormInputFileTemplate { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual FormLabelTemplate? FormLabelTemplate { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual FormOptionTemplate? FormOptionTemplate { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual PictureForm? PictureForm { get; set; }
}
