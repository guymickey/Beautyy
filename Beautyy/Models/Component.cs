using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Component")]
public partial class Component
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Banner? Banner { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Button? Button { get; set; }

    [InverseProperty("Component")]
    public virtual ICollection<CombineElement> CombineElements { get; set; } = new List<CombineElement>();

    [InverseProperty("IdNavigation")]
    public virtual Container? Container { get; set; }

    [InverseProperty("Component")]
    public virtual ICollection<Containing> Containings { get; set; } = new List<Containing>();

    [InverseProperty("IdNavigation")]
    public virtual FormTemplate? FormTemplate { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual GridForImage? GridForImage { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual GridTwoColumn? GridTwoColumn { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual ImageDesc? ImageDesc { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual ImageWithCaption? ImageWithCaption { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual OneTopicimagecaptionbutton? OneTopicimagecaptionbutton { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Sale? Sale { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual TableWithTopicAndDesc? TableWithTopicAndDesc { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual TextBox? TextBox { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual TwoTopicimagecaptionbutton? TwoTopicimagecaptionbutton { get; set; }
}
