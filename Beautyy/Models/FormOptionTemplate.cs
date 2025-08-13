using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("FormOptionTemplate")]
public partial class FormOptionTemplate
{
    [Key]
    public int Id { get; set; }

    public string? OptionValue { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("FormOptionTemplate")]
    public virtual FormElementTemplate IdNavigation { get; set; } = null!;
}
