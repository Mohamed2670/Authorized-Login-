using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MwTesting.Data;
using MwTesting.Model;

namespace MwTesting.Controllers
{
    [ApiController]
    [Route("users")]

    public class UsersController(JwtOptions jwtOptions, UserContext userContext) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public ActionResult<string> AuthUser(LoginM request)
        {
            var user = userContext.Set<User>().FirstOrDefault(x => x.Name == request.UserName && x.Password == request.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            Console.WriteLine("User : " + user.Name);
            var role = new Roles();

            var currentUserRole = user.Role == role.Admin ? role.Admin : role.User;
            Console.WriteLine("Role : " + currentUserRole);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {


                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                , SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name,user.Name),
                    new(ClaimTypes.Role,currentUserRole)
                })
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var final = tokenHandler.WriteToken(securityToken);
            return Ok(final);
        }
    }
}