using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("TwoTopicimagecaptionbutton")]
public partial class TwoTopicimagecaptionbutton
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("TwoTopicimagecaptionbutton")]
    public virtual Component IdNavigation { get; set; } = null!;
}
