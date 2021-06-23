using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Policies
{
    public class MinimalAgeHandler : AuthorizationHandler<MinimalAgeReq>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            MinimalAgeReq requirement)
        {
            if (context.User.HasClaim(claim => claim.Type.Equals("age")))
            {
                var age = Convert.ToByte(context.User
                .FindFirst(claim => claim.Type.Equals("age")).Value);

                if (age >= requirement.MinAge)
                {
                    context.Succeed(requirement);
                }
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
