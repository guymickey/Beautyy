using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Textfield")]
public partial class Textfield
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Textfield")]
    public virtual FormComponentTemplate IdNavigation { get; set; } = null!;
}
