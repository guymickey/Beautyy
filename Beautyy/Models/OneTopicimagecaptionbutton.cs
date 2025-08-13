using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("OneTopicimagecaptionbutton")]
public partial class OneTopicimagecaptionbutton
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("OneTopicimagecaptionbutton")]
    public virtual Component IdNavigation { get; set; } = null!;
}
