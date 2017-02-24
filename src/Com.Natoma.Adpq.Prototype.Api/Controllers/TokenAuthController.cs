using System;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.Auth;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Com.Natoma.Adpq.Prototype.Business.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Com.Natoma.Adpq.Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {
        private readonly IUserProfileService _userProfileService;

        public TokenAuthController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAuthToken([FromBody]UserProfileViewModel user)
        {
            // test the user

            var existingUser = await _userProfileService.Get(user.Email, user.Password);

            if (existingUser.Data != null)
            {
                var userData = (UserProfileViewModel) existingUser.Data;
                var requestAt = DateTime.Now;
                var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                var token = TokenAuthUtils.GenerateToken(userData, expiresIn);

                return Ok(new RequestResult
                {
                    State = RequestStateEnum.Success,
                    Data = new TokenAuthViewModel
                    {
                        UserProfileId = userData.UserProfileId,
                        Token = token
                    }
                });
            }
            else
            {
                return Ok(new RequestResult
                {
                    State = RequestStateEnum.Failed,
                    Msg = "Username or password is invalid"
                });
            }
        }
    }
}
