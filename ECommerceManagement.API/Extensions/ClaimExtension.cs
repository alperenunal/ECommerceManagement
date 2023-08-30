using System.Security.Claims;

namespace ECommerceManagement.API.Extensions
{
    public static class ClaimExtension
    {
        public static string GetSub(this ClaimsPrincipal user)
        {
            return user.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        }

        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
        }

        public static string GetName(this ClaimsPrincipal user)
        {
            return user.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        }

        public static string GetShopId(this ClaimsPrincipal user)
        {
            return user.Claims.First(claim => claim.Type == "shop_id").Value;
        }
    }
}
