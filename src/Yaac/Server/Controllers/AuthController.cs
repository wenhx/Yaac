using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly SignInManager<YaacUser> _signInManager;
        private readonly AuthOptions _authOptions;
        private static readonly string s_UserNameOrPasswordIncorrect = "The user name or password is incorrect";

        public AuthController(UserManager<YaacUser> userManager, SignInManager<YaacUser> signInManager, IOptions<AuthOptions> authOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions.Value;
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

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return BadRequest(InvokedResult.Fail(s_UserNameOrPasswordIncorrect));

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (signInResult.IsLockedOut)
                return BadRequest(InvokedResult.Fail("Account locked."));
            if (signInResult.IsNotAllowed)
                return BadRequest(InvokedResult.Fail("Account is not allowed to login."));
            if (signInResult.Succeeded)
                return Ok(InvokedResult.Ok(CreateToken(user)));

            return BadRequest(InvokedResult.Fail(s_UserNameOrPasswordIncorrect));
        }

        private object CreateToken(YaacUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.TokenKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}