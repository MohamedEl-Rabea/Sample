using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Moj.CMS.Shared.Runtime
{
    public class ApplicationSession : IApplicationSession
    {
        public ApplicationSession(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Claims = httpContextAccessor.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
        }

        public string UserId { get; }
        public Guid RequestId { get; set; }
        public string RequestName { get; set; }
        public List<KeyValuePair<string, string>> Claims { get; set; }
    }
}
