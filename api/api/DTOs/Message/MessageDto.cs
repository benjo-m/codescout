using api.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.DTOs.Message
{
    public class MessageDto
    {
        public MessageDto()
        {

        }

        public MessageDto(Models.Message message)
        {
            Id = message.Id;
            Text = message.Deleted == true ? "" : message.Text;
            DateSent = message.DateSent;
            Deleted = message.Deleted;
            SenderId = message.SenderId;
        }



        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateSent { get; set; }
        public bool Deleted { get; set; }

        public int SenderId { get; set; }
    }
}
