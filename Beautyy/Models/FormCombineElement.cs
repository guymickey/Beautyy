using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormCombineElement")]
public partial class FormCombineElement
{
    [Key]
    public int Id { get; set; }

    [Column("FormCompoentID")]
    public int? FormCompoentId { get; set; }

    [Column("FormElementID")]
    public int? FormElementId { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("FormCompoentId")]
    [InverseProperty("FormCombineElements")]
    public virtual FormComponent? FormCompoent { get; set; }

    [ForeignKey("FormElementId")]
    [InverseProperty("FormCombineElements")]
    public virtual FormElement? FormElement { get; set; }
}
