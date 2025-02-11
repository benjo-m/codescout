using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class FriendRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Sender))]
        public int SenderId { get; set; }
        public User Sender { get; set; }

        [ForeignKey(nameof(Receiver))]
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        public bool Accepted { get; set; }
    }
}
