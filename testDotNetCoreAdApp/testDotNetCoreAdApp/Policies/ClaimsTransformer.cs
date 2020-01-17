using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace testDotNetCoreAdApp.Policies
{
    public class ClaimsTransformation : IClaimsTransformation
    {
        private readonly IMicrosoftGraphService _graphService;

        private const string DefensiveClaimName = "ADGroupClaimsChecked";
        private const string ObjectIdClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        public ClaimsTransformation(IMicrosoftGraphService graphService)
        {
            _graphService = graphService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal Principal)
        {
            var claimsIdentity = Principal.Identity as ClaimsIdentity;
            var userIdentifier = FindClaimByType(claimsIdentity, ObjectIdClaimType);
            var alreadyAdded = AdGroupsAlreadyAdded(claimsIdentity);

            if (claimsIdentity == null || userIdentifier == null || alreadyAdded)
            {
                return Principal;
            }

            ICollection<string> UserGroupIds = await _graphService.GetUserGroupIds();

            foreach(string UserGroupId in UserGroupIds)
            {
                claimsIdentity.AddClaim(new Claim("groups", UserGroupId));
            }

            claimsIdentity.AddClaim(new Claim(DefensiveClaimName, "true"));

            return Principal;
        }

        private static string FindClaimByType(ClaimsIdentity claimsIdentity, string claimType)
        {
            return claimsIdentity?.Claims?.FirstOrDefault(c => c.Type.Equals(claimType, StringComparison.Ordinal))
                ?.Value;
        }

        private static bool AdGroupsAlreadyAdded(ClaimsIdentity claimsIdentity)
        {
            var alreadyAdded = FindClaimByType(claimsIdentity, DefensiveClaimName);
            var parsedSucceeded = bool.TryParse(alreadyAdded, out var valueWasTrue);

            return parsedSucceeded && valueWasTrue;
        }
    }
}