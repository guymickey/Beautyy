using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("TableWithTopicAndDesc")]
public partial class TableWithTopicAndDesc
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("TableWithTopicAndDesc")]
    public virtual Component IdNavigation { get; set; } = null!;
}
