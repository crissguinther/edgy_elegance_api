﻿namespace EdgyElegance.Domain.Entities;

public class Gender : BaseEntity {
    public string Name { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
