using System.Security.Claims;
using EMS.App.Common.Interface;

namespace EMS.Api.Service
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string? _id;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _id = httpContextAccessor.HttpContext?.User.FindFirstValue("UserId");
        }

        public Guid? Id => _id == null ? null : new Guid(_id);
        public string? Account => _httpContextAccessor.HttpContext?.User.FindFirstValue("Account");
    }
}