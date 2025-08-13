using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("ImageUploadwithImagecontent")]
public partial class ImageUploadwithImagecontent
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("ImageUploadwithImagecontent")]
    public virtual FormComponentTemplate IdNavigation { get; set; } = null!;
}
