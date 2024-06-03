using Generic_repo.Models.Interfaces;

namespace Generic_repo.Models
{
    public class Category :IBase
    {
        public int Id => CategoryId;
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public override string? ToString() => Name;
    }
}