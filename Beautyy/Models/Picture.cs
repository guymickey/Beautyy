using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("Picture")]
public partial class Picture
{
    [Key]
    public int Id { get; set; }

    public int? ImageId { get; set; }

    [Column("ImageURL")]
    public string? ImageUrl { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Picture")]
    public virtual ComponentElement IdNavigation { get; set; } = null!;

    [ForeignKey("ImageId")]
    [InverseProperty("Pictures")]
    public virtual ImageFile? Image { get; set; }
}
