using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;

namespace Moj.CMS.Web.Middlewares
{

    public class BlazorCookieLoginMiddleware
    {
        public static IDictionary<Guid, string> Refreshs { get; private set; }
            = new ConcurrentDictionary<Guid, string>();

        public static IDictionary<Guid, LoginDto> Logins { get; private set; }
            = new ConcurrentDictionary<Guid, LoginDto>();

        public static IDictionary<Guid, ClaimsPrincipal> Logouts { get; private set; }
            = new ConcurrentDictionary<Guid, ClaimsPrincipal>();

        private readonly RequestDelegate _next;

        public BlazorCookieLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SignInManager<CMSUser> signInMgr, UserManager<CMSUser> userManager)
        {
            if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
                await HandleLoginAsync(context, signInMgr);
            else if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("logoutKey"))
                await HandleLogoutAsync(context, signInMgr);
            else if (context.Request.Query.ContainsKey("refreshKey"))
                await HandleRefreshSignInAsync(context, signInMgr, userManager);

            await _next.Invoke(context);
        }

        private static async Task HandleLoginAsync(HttpContext context, SignInManager<CMSUser> signInMgr)
        {
            var key = Guid.Parse(context.Request.Query["key"]);
            var info = Logins[key];

            var result =
                await signInMgr.PasswordSignInAsync(info.UserName, info.Password, info.RememberMe, lockoutOnFailure: false);
            info.Password = null;
            if (result.Succeeded)
            {
                Logins.Remove(key);
                context.Response.Redirect("/");
            }
            else
            {
                //TODO: Proper error handling
                context.Response.Redirect("/loginfailed");
            }
        }

        private static async Task HandleLogoutAsync(HttpContext context, SignInManager<CMSUser> signInMgr)
        {
            try
            {
                var logoutKey = Guid.Parse(context.Request.Query["logoutKey"]);
                var claimsPrincipal = Logouts[logoutKey];
                if (signInMgr.IsSignedIn(claimsPrincipal))
                {
                    await signInMgr.SignOutAsync();
                    Logouts.Remove(logoutKey);
                }
            }
            catch (Exception e)
            {
                //TODO: Proper error handling
                context.Response.Redirect("/logoutFailed");
            }
        }
        private static async Task HandleRefreshSignInAsync(HttpContext context, SignInManager<CMSUser> signInMgr, UserManager<CMSUser> userManager)
        {
            var key = Guid.Parse(context.Request.Query["refreshKey"]);
            var userId = Refreshs[key];
            var user = await userManager.FindByIdAsync(userId);
            await signInMgr.RefreshSignInAsync(user);

        }
    }
}
