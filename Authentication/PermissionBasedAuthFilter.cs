using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MwTesting.Data;
using MwTesting.Model;

namespace MwTesting.Authentication
{
    public class PermissionBasedAuthFilter(UserContext userContext) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //as its the filter we filting the use here if he has the permission or not by validate the perm id with the user id as i pair (userid , perm id)
            // so if i have a pair like (1,1),(2,1) and the method need perm 1 so user 1 and 2 both of them can access this method 
            var attribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute);
            if (attribute != null)
            {
                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                else
                {
                    var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var hasPermissions = userContext.Set<UserPerm>().Any(x => x.UserId == userId && x.permission == attribute.Permisson);
                    Console.WriteLine("Hello : " + userId + " : " + hasPermissions);
                    if (!hasPermissions)
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }
        }
    }
}