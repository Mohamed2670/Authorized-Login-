using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace MwTesting.Authentication
{
    public class BasicAuth : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuth(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }
            var authHeader = Request.Headers["Authorization"].ToString();
            if(!authHeader.StartsWith("Basic ",StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unknown Scheme"));
            }
            var encodedCredentials = authHeader["Basic ".Length..].ToString();
            var decodedCredentails = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var UserAndPassword = decodedCredentails.Split(':');
            if (UserAndPassword[0] != "admin" || UserAndPassword[1] != "123")
                return Task.FromResult(AuthenticateResult.Fail("Wrong username or Password"));

            var identity = new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimTypes.Name,UserAndPassword[0]),
                new Claim(ClaimTypes.NameIdentifier,"1")
            },"Basic");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Basic");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}