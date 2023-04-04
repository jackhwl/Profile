﻿using System.ComponentModel.DataAnnotations;

namespace Wenlin.Domain.Entities;
public class Category
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
