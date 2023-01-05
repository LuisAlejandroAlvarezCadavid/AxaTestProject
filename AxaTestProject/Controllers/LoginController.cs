using AxaTestProject.Shared.Models.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AxaTestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
    public class LoginController : Controller
    {

        public IConfiguration configuration { get; set; }

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        [HttpPost]
        [Route("LoginUser")]
        public ActionResult LoginUser(LoginModel user)
        {
            if (user.User == "Alejo" && user.Password == "1234567")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Secret").Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: configuration.GetSection("JWT:ValidIssuer").Value, audience: configuration.GetSection("JWT:ValidAudience").Value,
                    claims: new List<Claim>() { new Claim(ClaimTypes.Name, user.User), new Claim(ClaimTypes.NameIdentifier, user.Password) },
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse
                {
                    Token = tokenString
                });
            }
            return Unauthorized();
        }
    }
}
