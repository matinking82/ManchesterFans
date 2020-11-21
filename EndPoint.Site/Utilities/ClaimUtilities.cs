using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndPoint.Site.Utilities
{
    public static class ClaimUtilities
    {
        public static int? GetUserId(this ClaimsPrincipal User)
        {
            if (User.Identity.IsAuthenticated)
            {
                return int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            return null;
        }

        public static int GetUserLevel(this ClaimsPrincipal User)
        {
            return int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Role).Value);
        }
    }
}
