using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OutlookTestBlazor.Services;


namespace OutlookTestBlazor.Services
{
    public class RoleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsManager()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.IsInRole("Manager") ?? false;
        }

        public bool IsAdmin()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.IsInRole("Admin") ?? false;
        }

        public bool HasRole(string role)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.IsInRole(role) ?? false;
        }
    }
}
