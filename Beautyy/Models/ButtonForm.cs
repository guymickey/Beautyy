using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("ButtonForm")]
public partial class ButtonForm
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("ButtonForm")]
    public virtual FormComponentTemplate IdNavigation { get; set; } = null!;
}
