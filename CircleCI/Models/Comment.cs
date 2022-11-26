using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircleCI.Models
{
    public class Comment
    {
        [Key]
        public int IdComment { get; set; }
        [Column(TypeName = "TEXT")]
        public string CommentContent { get; set; } = string.Empty;
        [ForeignKey("User")]
        public int? IdUser { get; set; }
        [ForeignKey("Post")]
        public int? IdPost { get; set; }
        public User? User { get; set; }
        public Post? Post { get; set; }
    }
}
