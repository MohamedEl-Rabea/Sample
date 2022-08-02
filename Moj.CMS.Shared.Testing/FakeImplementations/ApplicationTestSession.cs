using Moj.CMS.Shared.Runtime;
using System;

namespace Moj.CMS.Shared.Testing.FakeImplementation
{
    public class ApplicationTestSession : IApplicationSession
    {
        public string UserId => "1";
        public Guid RequestId { get; set; }
        public string RequestName { get; set; }
    }
}
