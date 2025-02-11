using api.Data;
using api.DTOs.Message;
using api.DTOs.User;
using api.Models;

namespace api.Services
{
    public class MessageService
    {
        private readonly ApplicationContext _context;
        public int MessagesToReturn { get { return 12; } }


        public MessageService(ApplicationContext context)
        {
            _context = context;
        }







        public bool isUserValid(int userId)
        {
            return userId > 0;
        }

        public bool areUsersValid(int senderId, int receiverId)
        {
            return (senderId != receiverId) && isUserValid(senderId) && isUserValid(receiverId);
        }







        public List<MessageDto> GetMessages(int senderId, int receiverId, int? messageFromId = null, int? messageToId = null)
        {
            if ((messageFromId != null) && (messageToId != null))    return new List<MessageDto>();

            IOrderedQueryable<MessageDto>? query;

            try
            {
                query = SelectMessageDto(_context.Messages
                    .Where(x =>
                            ((x.SenderId == senderId) || (x.SenderId == receiverId))
                            && ((x.ReceiverId == receiverId) || x.ReceiverId == senderId)
                            && (x.DateSent < ((messageFromId == null) ? DateTime.Now : _context.Messages.Find(messageFromId).DateSent))
                            && ((messageToId == null) ? true : (x.DateSent > _context.Messages.Find(messageToId).DateSent))
                        )
                    )
                    .OrderByDescending(x => x.DateSent);
            }
            catch (Exception ex)
            {
                return new List<MessageDto>();
            }



            return (messageToId == null) ? query.Take(MessagesToReturn).ToList() : query.ToList();
        }



        public List<MessageDto> GetNewMessages(int senderId, int receiverId, int? messageToId)
        {
            List<MessageDto> msglist = new List<MessageDto>();
            List<Message> notReceivedList;

            try
            {
                notReceivedList = _context.Messages
                    .Where(x =>
                            ((x.SenderId == senderId) || (x.SenderId == receiverId))
                            && ((x.ReceiverId == receiverId) || x.ReceiverId == senderId)
                            && ((messageToId == null) ? true : (x.DateSent > _context.Messages.Find(messageToId).DateSent))
                        )
                    .OrderByDescending(x => x.DateSent)
                    .ToList();
            }
            catch (Exception ex)
            {
                return new List<MessageDto>();
            }


            notReceivedList.ToList().ForEach(x =>
            {
                if(x.ReceiverId == senderId) x.Received = true;

                msglist.Add(new MessageDto(x));
            });

            if(msglist.Count() > 0) _context.SaveChanges();


            return msglist;
        }




        // combines all not received messages and received messages (number determined by MessagesToReturn property)
        public List<MessageDto> GetRecentMessages(int senderId, int receiverId)
        {
            List<MessageDto> msglist = new List<MessageDto>();

            var notReceivedQuery = _context.Messages
                .Where(x =>
                    ((x.SenderId == senderId) || (x.SenderId == receiverId))
                    && ((x.ReceiverId == receiverId) || x.ReceiverId == senderId)
                    && (x.Received == false)
                )
                .OrderByDescending(x => x.DateSent);

            
            notReceivedQuery.ToList().ForEach(x =>
            {
                x.Received = true;

                msglist.Add(new MessageDto(x));
            });


            if (msglist.Count() > 0)
            {
                var lastMessageId = msglist.ElementAt(msglist.Count() - 1).Id;

                try
                {
                    SelectMessageDto(_context.Messages
                        .Where(x =>
                            (x.Received == true)
                            && (x.DateSent < _context.Messages.Find(lastMessageId).DateSent)
                        )
                    )
                    .OrderByDescending(x => x.DateSent)
                    .Take(MessagesToReturn)
                    .ToList()
                    .ForEach(x =>
                     {
                         msglist.Add(x);
                     });
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                msglist = GetMessages(senderId, receiverId, null);
            }


            _context.SaveChanges();

            return msglist;
        }



        public List<UserMessagesResponse> GetLastMessageWithAllUsers(int userId)
        {
            List<UserMessagesResponse> usrsmsgs = new();

            /*_context.Messages
                .Where(m=> (m.SenderId==senderId) || (m.ReceiverId==senderId))
                .Join(_context.Users , msg=>msg.SenderId, user=>user.Id, (msg, user) => new { msg, user})
                .GroupBy(usermsg=>usermsg.user.Id)
                .Select(usermsg=> new { usermsg.Key, usermsg. })*/

            var um = _context.Messages
                .Where(m => (m.SenderId == userId) || (m.ReceiverId == userId))
                //.Join(_context.Users, msg => msg.SenderId, user => user.Id, (msg, user) => new { msg, user })
                .Select(msg => new { msg.SenderId, msg.ReceiverId })
                .GroupBy(usermsg => new { usermsg.SenderId, usermsg.ReceiverId })
                .Select(usermsg => new
                {
                    userId = (usermsg.Key.SenderId == userId) ? usermsg.Key.ReceiverId : usermsg.Key.SenderId,
                    message = _context.Messages.Where(x => ((x.SenderId == usermsg.Key.SenderId) || (x.SenderId == usermsg.Key.ReceiverId)) && ((x.ReceiverId == usermsg.Key.SenderId) || (x.ReceiverId == usermsg.Key.ReceiverId))).OrderByDescending(x => x.DateSent).Take(1).ElementAt(0)
                }).ToList();

            um.ForEach(x=>
            {
                if (!usrsmsgs.Exists(usrmsg => usrmsg.userId == x.userId))
                {
                    var umr = new UserMessagesResponse();
                    umr.userId = x.userId;
                    umr.username = _context.Users.Find(umr.userId)?.Username;
                    umr.messages.Add(new MessageDto(x.message));

                    usrsmsgs.Add(umr);
                }
            });

            return usrsmsgs;
        }



        public List<UserMessagesResponse> GetNewMessageWithAllUsers(int userId)
        {
            List<UserMessagesResponse> usrsmsgs = new();

            var um =_context.Messages
                .Where(m => (m.ReceiverId == userId) && (!m.Received))
                //.Join(_context.Users, msg => msg.SenderId, user => user.Id, (msg, user) => new { msg, user })
                .Select(msg => new { msg.SenderId, msg.ReceiverId })
                .GroupBy(usermsg => new { usermsg.SenderId, usermsg.ReceiverId })
                .Select(usermsg => new
                {
                    userId = usermsg.Key.SenderId,
                    message = _context.Messages.Where(x => x.ReceiverId == usermsg.Key.ReceiverId).OrderByDescending(x => x.DateSent).Take(1).ElementAt(0)
                }).ToList();

            um.ForEach(x =>
            {
                if (!usrsmsgs.Exists(usrmsg => usrmsg.userId == x.userId))
                {
                    var umr = new UserMessagesResponse();
                    umr.userId = x.userId;
                    umr.username = _context.Users.Find(umr.userId)?.Username;
                    umr.messages.Add(new MessageDto(x.message));

                    usrsmsgs.Add(umr);
                }
            });

            return usrsmsgs;
        }






        public int AddMessage(int senderId, int receiverId, String text)
        {
            _context.Messages
                .Add(new Message { Id = 0, Text = text, DateSent = DateTime.Now, Received = false, Deleted = false, SenderId = senderId, ReceiverId = receiverId});

            return _context.SaveChanges();
        }








        private IQueryable<MessageDto> SelectMessageDto(IQueryable<Message> messages)
        {
            return messages.Select(x => new MessageDto { Id = x.Id, Text = ((x.Deleted == true) ? "" : x.Text), DateSent = x.DateSent, Deleted = x.Deleted, SenderId = x.SenderId });
        }
    }
}
