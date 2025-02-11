using api.Data;
using api.DTOs.FriendRequest;
using api.DTOs.User;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System.Runtime.InteropServices;

namespace api.Services
{
    public class FriendService
    {
        private readonly int usersPerSearch = 10;
        private readonly int friendRequestsPerRequest = 10;

        private readonly ApplicationContext _context;

        public FriendService(ApplicationContext context)
        {
            _context = context;
        }







        public bool isIdValid(int userId)
        {
            return userId > 0;
        }

        public bool areUsersValid(int senderId, int receiverId)
        {
            return (senderId != receiverId) && isIdValid(senderId) && isIdValid(receiverId);
        }








        public UserSearchResponse CreateUserSearchResponse(int id, string name)
        {
            return new UserSearchResponse { Id = id, Name = name };
        }

        public FriendRequestResponse CreateFriendRequestResponse(int id, string senderName)
        {
            return new FriendRequestResponse { Id = id, SenderName = senderName };
        }






        public bool? IsFriend(int askerId, int askedId)
        {
            var fr = _context.FriendRequests.FirstOrDefault(fr => ((fr.SenderId == askerId) && (fr.ReceiverId == askedId)) || ((fr.SenderId == askedId) && (fr.ReceiverId == askerId)));

            return (fr == null) ? null : fr.Accepted;
        }






        public List<UserSearchResponse> GetUsersByName(int searcherId, string name, bool searchFriendsOnly, int page = 1)
        {
            List<UserSearchResponse> friends = new List<UserSearchResponse>();

            if((page == 0) || ((!searchFriendsOnly) && (name.Length == 0)))
            {
                return friends;
            }

            if (searchFriendsOnly)
            {
                var qres = _context.FriendRequests
                .Where(fr => ((fr.SenderId == searcherId) || (fr.ReceiverId == searcherId)) && (fr.Accepted == true))
                .Include(fr => fr.Sender)
                .Include(fr => fr.Receiver)
                .Select(fr => new { Id = ((fr.SenderId != searcherId) ? fr.SenderId : fr.ReceiverId), Name = ((fr.SenderId != searcherId) ? fr.Sender.Username : fr.Receiver.Username) })
                .Where(fr => fr.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(fr => fr.Name)
                .Skip((page - 1) * usersPerSearch)
                .Take(usersPerSearch)
                .ToList();

                qres.ForEach(x =>
                {
                    friends.Add(CreateUserSearchResponse(x.Id, x.Name));
                });
            }
            else
            {
                var qres = _context.Users
                    .Where(u => (u.Id != searcherId) && u.Username.ToLower().Contains(name.ToLower()))
                    .OrderBy(u => u.Username)
                    .Skip((page - 1) * usersPerSearch)
                    .Take(usersPerSearch)
                    .ToList();

                qres.ForEach(x =>
                {
                    friends.Add(CreateUserSearchResponse(x.Id, x.Username));
                });
            }

            return friends;
        }




        public List<FriendRequestResponse> GetFriendRequests(int toUserId, int page = 1)
        {
            List<FriendRequestResponse> fr = new List<FriendRequestResponse>();

            if (page == 0)
            {
                return fr;
            }

            try
            {
                var qres = _context.FriendRequests
                .Where(fr => (fr.ReceiverId == toUserId) && (fr.Accepted == false))
                .OrderByDescending(fr => fr.Id)
                .Include(fr => fr.Sender)
                .Skip((page - 1) * friendRequestsPerRequest)
                .Take(friendRequestsPerRequest)
                .ToList();

                qres.ForEach(x =>
                {
                    fr.Add(CreateFriendRequestResponse(x.Id, x.Sender.Username));
                });

                return fr;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }





        public void AddFriendRequest(int senderId, int receiverId)
        {
            if (!areUsersValid(senderId, receiverId))
            {
                throw new Exception();
            }

            var existingReq = _context.FriendRequests.FirstOrDefault(fr => ((fr.SenderId == senderId) || (fr.SenderId == receiverId)) && ((fr.ReceiverId == receiverId) || (fr.ReceiverId == senderId)));

            if (existingReq != null)
            {
                if (existingReq.Accepted == true)
                {
                    throw new Exception();
                }
                else if (existingReq.ReceiverId == senderId)
                {
                    existingReq.Accepted = true;
                }

            }
            else
            {
                _context.FriendRequests.Add(new FriendRequest { SenderId = senderId, ReceiverId = receiverId, Accepted = false });
            }

            if (_context.SaveChanges() == 0) throw new Exception();
        }




        public int AcceptFriendRequest(int requestId)
        {
            var req = _context.FriendRequests.FirstOrDefault(fr => fr.Id == requestId);

            if (req == null) throw new Exception();

            req.Accepted = true;

            if (_context.SaveChanges() == 0) throw new Exception();

            return req.Id;
        }







        public int RemoveFriendRequest(int requestId)
        {
            var req = _context.FriendRequests.FirstOrDefault(fr => fr.Id == requestId);

            if (req == null) throw new Exception();

            return RemoveFriendRequest(req);
        }



        public int RemoveFriendRequest(int senderId, int toRemoveId)
        {
            var req = _context.FriendRequests.FirstOrDefault(fr => ((fr.SenderId == senderId) && (fr.ReceiverId == toRemoveId)) || ((fr.SenderId == toRemoveId) && (fr.ReceiverId == senderId)));

            if (req == null) throw new Exception();

            return RemoveFriendRequest(req);
        }



        private int RemoveFriendRequest(FriendRequest fr)
        {
            fr = _context.FriendRequests.Remove(fr).Entity;

            if (_context.SaveChanges() == 0) throw new Exception();

            return fr.Id;
        }
    }
}
