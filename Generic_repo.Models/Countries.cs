using Generic_repo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Generic_repo.Models
{
    public class Countries :IBase
    {
        public int Id => CountryId;
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
