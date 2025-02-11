using api.DTOs.FriendRequest;
using api.DTOs.User;
using api.Migrations;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly FriendService _friendService;
        private readonly AuthService _authService;


        public FriendController(FriendService frq, AuthService authService)
        {
            _friendService = frq;
            _authService = authService;

        }





        [HttpGet("IsFriend")]
        public ActionResult IsFriend(int askerId, int askedId)
        {
            if (!_friendService.areUsersValid(askerId, askedId)) return BadRequest();

            return Ok(_friendService.IsFriend(askerId, askedId));
        }





        [HttpGet("SearchUsersByName")]
        public ActionResult<List<UserSearchResponse>> GetUsersByName(int searcherId, string name = "", bool searchFriendsOnly = true, int page = 1)
        {
            if (!_authService.isValidId(searcherId)) return BadRequest();
            else if (!_authService.AuthenticatedUser()) return Unauthorized();

            try
            {
                return Ok(_friendService.GetUsersByName(searcherId, name, searchFriendsOnly, page));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }




        [HttpGet("GetFriendRequests")]
        public ActionResult<List<FriendRequestResponse>> GetFriendRequests(int toUserId, int page = 1)
        {
            if (!_authService.isValidId(toUserId)) return BadRequest();
            else if (!_authService.AuthenticatedUser()) return Unauthorized();

            try
            {
                return Ok(_friendService.GetFriendRequests(toUserId, page));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }





        [HttpPost("SendFriendRequest")]
        public ActionResult SendFriendRequest(int senderId, int receiverId)
        {
            try
            {
                if (!_authService.isValidId(senderId)) return BadRequest();
                else if (!_authService.AuthenticatedUser()) return Unauthorized();

                _friendService.AddFriendRequest(senderId, receiverId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("RespondToFriendRequest")]
        public ActionResult<int> RespondToFriendRequest(int requestId, bool accepted = false)
        {
            try
            {
                if (!_authService.isValidId(requestId)) return BadRequest();
                else if (!_authService.AuthenticatedUser()) return Unauthorized();

                if (accepted == true)
                {
                    return Ok(_friendService.AcceptFriendRequest(requestId));
                }
                else
                {
                    return Ok(_friendService.RemoveFriendRequest(requestId));
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPost("RemoveFriend")]
        public ActionResult<int> RemoveFriend(int senderId, int toRemoveId)
        {
            if(!_friendService.areUsersValid(senderId, toRemoveId)) return BadRequest();

            return Ok(_friendService.RemoveFriendRequest(senderId, toRemoveId));
        }
    }
}
