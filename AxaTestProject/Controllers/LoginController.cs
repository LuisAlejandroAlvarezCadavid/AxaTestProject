using AxaTestProject.Repositories.DataEntities;
using AxaTestProject.Resources;
using AxaTestProject.Services.Interfaces;
using AxaTestProject.Shared.Models.Login;
using Microsoft.AspNetCore.Identity;
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
        public IPasswordHasher<IdentityUser> CreateHashPassword { get; set; }
        public ILoginService LoginService { get; set; }



        public LoginController(IConfiguration configuration, IPasswordHasher<IdentityUser> createHashPassword, ILoginService loginService)
        {
            this.configuration = configuration;
            CreateHashPassword = createHashPassword;
            LoginService = loginService;
        }


        [HttpPost]
        [Route("LoginUser")]
        public async Task<ActionResult> LoginUser(LoginModel user)
        {
            LoginUserEntity loginUserEntity = await LoginService.GetUserLoginPassWordAsync(user.User!);
            IdentityUser _user = new IdentityUser();
            PasswordVerificationResult passResult = CreateHashPassword.VerifyHashedPassword(_user, loginUserEntity?.Password!, user.Password!);
            if (passResult == PasswordVerificationResult.Success)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Secret").Value!));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: configuration.GetSection("JWT:ValidIssuer").Value, audience: configuration.GetSection("JWT:ValidAudience").Value,
                    claims: new List<Claim>() { new Claim(ClaimTypes.Name, user.User!), new Claim(ClaimTypes.NameIdentifier, user.Password!) },
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse
                {
                    Token = tokenString
                });
            }
            else
            {
                return BadRequest(HttpMessages.UserDontExist);
            }
        }
    }
}
