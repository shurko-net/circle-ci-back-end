using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircleCI.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Surname { get; set; } = string.Empty;
        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Email { get; set; } = string.Empty;
        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Password { get; set; } = string.Empty;
        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string TNumber { get; set; } = string.Empty;
        [Column(TypeName = "TEXT")]
        public string Biography { get; set; } = string.Empty;
        public int Subscribed { get; set; }
    }
}
