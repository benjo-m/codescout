using api.DTOs.Message;

namespace api.DTOs.User
{
    public class UserMessagesResponse
    {
        public int userId { get; set; }
        public string username { get; set; }
        public List<MessageDto> messages { get; set; } = new List<MessageDto>();
    }
}
