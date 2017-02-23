using System;
using Com.Natoma.Adpq.Prototype.Business.Models.Auth;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Com.Natoma.Adpq.Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {
        [HttpPost]
        public string GetAuthToken([FromBody]UserProfileViewModel user)
        {
            // test the user
            //var existUser = UserStorage.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            var existingUser = new UserProfileViewModel();

            if (existingUser != null)
            {
                var requestAt = DateTime.Now;
                var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                var token = TokenAuthUtils.GenerateToken(existingUser, expiresIn);

                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestStateEnum.Success,
                    Data = new
                    {
                        requertAt = requestAt,
                        expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                        tokenType = TokenAuthOption.TokenType,
                        accessToken = token
                    }
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestStateEnum.Failed,
                    Msg = "Username or password is invalid"
                });
            }
        }
    }
}
