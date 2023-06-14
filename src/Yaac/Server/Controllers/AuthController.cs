using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yaac.Server.Models;
using Yaac.Shared;
using Yaac.Shared.Models;

namespace Yaac.Server.Controllers
{
    [Route("api/auth/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<YaacUser> _userManager;

        public AuthController(UserManager<YaacUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var user = new YaacUser { UserName = model.UserName };
            var createdResult = await _userManager.CreateAsync(user, model.Password);
            if (!createdResult.Succeeded)
            {
                var errors = String.Join(Environment.NewLine, createdResult.Errors.Select(err => err.Description));
                return BadRequest(InvokedResult.Fail("User creation failed.", errors));
            }

            return Ok(InvokedResult.Success);
        }
    }
}