using api.DTOs.Message;
using api.DTOs.User;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;


        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }




        [HttpGet("GetMessages")]
        public ActionResult<List<MessageDto>> GetMessages(int senderId, int receiverId, int? messageFromId = null, int? messageToId = null)
        {
            //DateTime? df = ((dateFrom == null) ? null : DateTime.Parse(dateFrom)), dt = ((dateTo == null) ? null : DateTime.Parse(dateTo));

            if (_messageService.areUsersValid(senderId, receiverId)) return _messageService.GetMessages(senderId, receiverId, messageFromId, messageToId);
            else return BadRequest();
        }



        [HttpGet("GetRecentMessages")]
        public ActionResult<List<MessageDto>> GetRecentMessages(int senderId, int receiverId)
        {
            if (_messageService.areUsersValid(senderId, receiverId)) return _messageService.GetRecentMessages(senderId, receiverId);
            else return BadRequest();
        }




        [HttpGet("GetNewMessages")]
        public ActionResult<List<MessageDto>> GetNewMessages(int senderId, int receiverId, int? messageToId = null)
        {
            //DateTime? dt = ((dateTo == null) ? null : DateTime.Parse(dateTo));

            if (_messageService.areUsersValid(senderId, receiverId)) return _messageService.GetNewMessages(senderId, receiverId, messageToId);
            else return BadRequest();
        }




        [HttpGet("GetLastMessageWithAllUsers")]
        public ActionResult<List<UserMessagesResponse>> GetLastMessageWithAllUsers(int userId)
        {
            //DateTime? dt = ((dateTo == null) ? null : DateTime.Parse(dateTo));

            if (_messageService.isUserValid(userId)) return _messageService.GetLastMessageWithAllUsers(userId);
            else return BadRequest();
        }




        [HttpGet("GetNewMessageWithAllUsers")]
        public ActionResult<List<UserMessagesResponse>> GetNewMessageWithAllUsers(int userId)
        {
            //DateTime? dt = ((dateTo == null) ? null : DateTime.Parse(dateTo));

            if (_messageService.isUserValid(userId)) return _messageService.GetNewMessageWithAllUsers(userId);
            else return BadRequest();
        }



        /*[HttpGet("GetRecentMessagesPerUser")]
        public ActionResult<List<MessageDto>> GetRecentMessagePerUser(int senderId, int receiverId)
        {
            if (_messageService.areUsersValid(senderId, receiverId)) return _messageService.GetRecentMessages(senderId, receiverId);
            else return BadRequest();
        }




        [HttpGet("GetNewMessagesPerUser")]
        public ActionResult<List<MessageDto>> GetNewMessagePerUser(int senderId, int receiverId, int? messageToId = null)
        {
            //DateTime? dt = ((dateTo == null) ? null : DateTime.Parse(dateTo));

            if (_messageService.areUsersValid(senderId, receiverId)) return _messageService.GetNewMessages(senderId, receiverId, messageToId);
            else return BadRequest();
        }*/








        [HttpPost("SendMessage")]
        public ActionResult SendMessage(int senderId, int receiverId, [FromBody] String text)
        {
            if ((!_messageService.areUsersValid(senderId, receiverId)) || (_messageService.AddMessage(senderId, receiverId, text) == 0))    return BadRequest();
            else return Ok();
        }
    }
}
