using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircleCI.Models
{
    public class Subscription
    {
        [Key]
        public int IdSubscription { get; set; }
        [ForeignKey("User")]
        public int? IdUser { get; set; }
        public int IdUserSub { get; set; }
        public User? User { get; set; }
    }
}
