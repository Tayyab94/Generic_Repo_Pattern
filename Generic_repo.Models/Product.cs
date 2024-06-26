﻿using Generic_repo.Models;
using Generic_repo.Models.Interfaces;

namespace Generic_repo.Models
{
    public class Product : IBase
    {
        public int Id => ProductId;
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public override string? ToString() => Name;
    }
}
