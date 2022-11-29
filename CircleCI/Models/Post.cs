using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircleCI.Models
{
    public class Post
    {
        [Key]
        public int IdPost { get; set; }
        [ForeignKey("User")]
        public int? IdUser { get; set; }
        [ForeignKey("Category")]
        public int? IdCategory { get; set; } 
        public DateTime Date { get; set; }
        [Column(TypeName = "TEXT")]
        public string PostContent { get; set; } = string.Empty;
        public int Likes { get; set; }
        public User? User { get; set; }
        public Category? Category { get; set; }
    }
}
