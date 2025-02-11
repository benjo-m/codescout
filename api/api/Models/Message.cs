using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Text { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        [Required]
        public bool Received { get; set; }

        [Required]
        public bool Deleted { get; set; }

        //[ForeignKey("Sender")]
        public int SenderId { get; set; }
        //public User Sender { get; set; }

        //[ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        //public User Receiver { get; set; }
    }
}
