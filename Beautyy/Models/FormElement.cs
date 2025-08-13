using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormElement")]
public partial class FormElement
{
    [Key]
    public int Id { get; set; }

    [Column("FormElementTeplateID")]
    public int? FormElementTeplateId { get; set; }

    public bool IsDelete { get; set; }

    public string? ElementForm { get; set; }

    [InverseProperty("FormElement")]
    public virtual ICollection<FormCombineElement> FormCombineElements { get; set; } = new List<FormCombineElement>();

    [ForeignKey("FormElementTeplateId")]
    [InverseProperty("FormElements")]
    public virtual FormElementTemplate? FormElementTeplate { get; set; }
}
