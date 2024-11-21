using System.Security.Claims;

namespace First_Project.Models.repository
{
    public static class ClaimsPrincipalExtensions
    {


        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }


    }
}




