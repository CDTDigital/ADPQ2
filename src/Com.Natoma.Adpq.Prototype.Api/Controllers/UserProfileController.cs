using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Com.Natoma.Adpq.Prototype.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            // return a list of Userprofiles?
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _userProfileService.Create(new UserProfileViewModel());
            return Ok(await _userProfileService.Get(id)); 
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserProfileViewModel userProfileViewModel)
        {
            var result = await _userProfileService.Create(userProfileViewModel);
            // create a new user profile
            return CreatedAtAction("CreateProfile", new {id = result.UserProfileId, result = result});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserProfileViewModel userProfileViewModel)
        {
            // update user profile
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            // soft delete a user profile?
            return Ok();
        }
    }
}
