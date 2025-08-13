using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Imageupload")]
public partial class Imageupload
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Imageupload")]
    public virtual FormComponentTemplate IdNavigation { get; set; } = null!;
}
