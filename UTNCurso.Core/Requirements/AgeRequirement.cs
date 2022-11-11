using Microsoft.AspNetCore.Authorization;

namespace UTNCurso.Core.Requirements
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public bool IsActive { get; set; }
        public int Limit { get; set; }

        public AgeRequirement(bool isActive, int limit)
        {
            IsActive = isActive;
            Limit = limit;
        }
    }
}
