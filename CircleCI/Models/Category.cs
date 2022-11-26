using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircleCI.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Name { get; set; } = string.Empty;
    }
}
