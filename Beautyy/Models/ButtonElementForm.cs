using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("ButtonElementForm")]
public partial class ButtonElementForm
{
    [Key]
    public int Id { get; set; }

    public string? ButtonText { get; set; }

    [Column("ButtonURL")]
    public string? ButtonUrl { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("ButtonElementForm")]
    public virtual FormElementTemplate IdNavigation { get; set; } = null!;
}
