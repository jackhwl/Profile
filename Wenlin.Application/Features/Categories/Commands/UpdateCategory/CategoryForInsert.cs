﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Categories.Commands.UpdateCategory;
public class CategoryForInsert
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
