using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("PictureForm")]
public partial class PictureForm
{
    [Key]
    public int Id { get; set; }

    public int? ImageId { get; set; }

    public string? ImageUrl { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("PictureForm")]
    public virtual FormElementTemplate IdNavigation { get; set; } = null!;

    [ForeignKey("ImageId")]
    [InverseProperty("PictureForms")]
    public virtual ImageFile? Image { get; set; }
}
