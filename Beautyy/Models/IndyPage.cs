using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("IndyPage")]
public partial class IndyPage
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("IndyPage")]
    public virtual Container IdNavigation { get; set; } = null!;
}
