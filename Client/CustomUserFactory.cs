using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.Logging;

namespace TeamPlanner.Client
{
    public class CustomUserFactory
        : AccountClaimsPrincipalFactory<CustomUserAccount>
    {
        private readonly ILogger<CustomUserFactory> logger;
        public CustomUserFactory(IAccessTokenProviderAccessor accessor,
            ILogger<CustomUserFactory> logger)
            : base(accessor)
        {
            this.logger = logger;
        }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(
            CustomUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);
            if (initialUser.Identity is null)
                throw new NullReferenceException();

            if (initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = (ClaimsIdentity)initialUser.Identity;

                foreach (var role in account.Roles)
                {
                    userIdentity.AddClaim(new Claim("role", role));
                }
            }

            return initialUser;
        }
    }
}