using Microsoft.AspNetCore.Authorization;

namespace UTNCurso.Core.Requirements
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            if (requirement.IsActive)
            {
                var age = context.User.FindFirst("Age");

                if (int.Parse(age?.Value ?? "0") >= requirement.Limit)
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
