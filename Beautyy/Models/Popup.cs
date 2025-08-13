using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Popup")]
public partial class Popup
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Popup")]
    public virtual FormComponentTemplate IdNavigation { get; set; } = null!;
}
