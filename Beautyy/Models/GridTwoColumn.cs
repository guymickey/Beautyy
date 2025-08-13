using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("GridTwoColumn")]
public partial class GridTwoColumn
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("GridTwoColumn")]
    public virtual Component IdNavigation { get; set; } = null!;
}
