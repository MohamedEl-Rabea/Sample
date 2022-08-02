using Microsoft.AspNetCore.Http;
using Serilog;
using System.Linq;

namespace Moj.CMS.Settings.Shared.Extensions
{
    /// <summary>
    /// Enriches the HTTP request log with additional data via the Diagnostic Context
    /// </summary>
    /// <param name="diagnosticContext">The Serilog diagnostic context</param>
    /// <param name="httpContext">The current HTTP Context</param>
    public class LogEnricher
    {
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());

            // to set any thrown exception
            //var email = httpContext.User.FindFirst(ClaimTypes.Email)?.Value; // if we need email
            var userName = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "Guest"; //Gets user Name from user Identity  
            diagnosticContext.Set("UserName", userName); // log it authmatically for any default thrown exception 
        }

    }
}
