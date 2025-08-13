using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("GridForImage")]
public partial class GridForImage
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("GridForImage")]
    public virtual Component IdNavigation { get; set; } = null!;
}
