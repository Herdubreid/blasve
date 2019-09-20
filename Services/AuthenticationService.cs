using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace blasve.Services
{
    public class AuthenticationService : AuthenticationStateProvider
    {
        public E1Service E1Service { get; }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = E1Service.AuthResponse == null
                ? new ClaimsPrincipal()
                : new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, E1Service.AuthResponse.username)
                }, "JdeAuthentication"));
            return Task.FromResult(new AuthenticationState(user));
        }
        async public Task<string> Login(string user, string password)
        {
            E1Service.AuthRequest.username = user;
            E1Service.AuthRequest.password = password;
            try
            {
                await E1Service.AuthenticateAsync();
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return string.Empty;
        }
        async public Task Logout()
        {
            await E1Service.LogoutAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public AuthenticationService(E1Service e1Service)
        {
            E1Service = e1Service;
        }
    }
}
