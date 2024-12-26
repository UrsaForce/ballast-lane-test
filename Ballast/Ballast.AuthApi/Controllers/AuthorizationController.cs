using Ballast.Data.DTO.Authorization;
using Ballast.Data.Entities.Identity;
using Ballast.Service.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ballast.AuthApi.Controllers
{
    [ApiController]
    [Route("authapi/[controller]")]
    public class AuthorizationController : Controller
    {
        private AuthorizationOptions _options;
        private readonly UserManager _userManager;
        public AuthorizationController(IOptionsMonitor<AuthorizationOptions> optionsMonitor)
        {
            _options = optionsMonitor.CurrentValue;
            _userManager = new UserManager();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill up required data.");
            }
            var response = new LoginResponse();
            try
            {
                var user = _userManager.VerifyUserLogin(loginRequest.Username, loginRequest.Password);
                response.IsSuccess = true;
                response.Token = IssueToken(user);
                return Ok(response);
            }
            catch(Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpRequest signUpRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill up required data.");
            }
            var response = new SignUpResponse();
            try
            {
                var user = _userManager.RegisterUser(signUpRequest.Username, signUpRequest.Password, signUpRequest.FirstName, signUpRequest.LastName);
                response.IsSuccess = true;
                response.Message = $"User {signUpRequest.Username} registered successfully.";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
                return BadRequest(response);
            }
        }

        private string IssueToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: GetUserClaims(user),
                expires: DateTime.Now.AddMinutes(_options.ExpirationMinutes),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetUserClaims(User user)
        {
            return new List<Claim>
            {
                new Claim("User_Id", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
        }

        protected override void Dispose(bool disposing)
        {
            _userManager.Dispose();
            base.Dispose(disposing);
        }
    }
}
