using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using MovieApp.Domain.Entities;
using System.Security.Claims;

namespace MovieApp.Web.Components
{
    public class CustomAuthStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;
        
        public CustomAuthStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory)
            : base(loggerFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);
        
        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var userPrincipal = authenticationState.User;

            if (userPrincipal.Identity?.IsAuthenticated == true)
            {
                var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await userManager.FindByIdAsync(userId);

                return user != null;
            }
            
            return true;
        }
    }
}