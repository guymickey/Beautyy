﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

[Table("About")]
public partial class About
{
    [Key]
    public int Id { get; set; }
}
