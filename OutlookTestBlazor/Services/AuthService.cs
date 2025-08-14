
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web;
using System.Security.Claims;

public class AuthService
{
    private readonly AuthenticationStateProvider _provider;
    private readonly IHttpContextAccessor _http;

    public AuthService(AuthenticationStateProvider provider, IHttpContextAccessor http)
    {
        _provider = provider;
        _http = http;
    }

    public async Task<string?> GetUserEmailAsync()
    {
        var authState = await _provider.GetAuthenticationStateAsync();
        return authState.User?.FindFirstValue(ClaimTypes.Email);
    }

    public bool IsAuthenticated()
    {
        return _http.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
