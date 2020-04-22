using System.Security.Claims;

namespace ReqResLog.HttpLogger.Models
{
    internal class UserData
    {
        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }

        public static UserData Create(ClaimsPrincipal user)
        {
            if (user?.Identity == null)
                return new UserData();

            return new UserData
            {
                AuthenticationType = user.Identity.AuthenticationType,
                IsAuthenticated = user.Identity.IsAuthenticated,
                Name = user.Identity.Name,
            };
        }
    }
}
