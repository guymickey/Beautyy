using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("ImageFile")]
public partial class ImageFile
{
    [Key]
    public int Id { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("Image")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [InverseProperty("Image")]
    public virtual ICollection<PictureForm> PictureForms { get; set; } = new List<PictureForm>();

    [InverseProperty("Image")]
    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
}
