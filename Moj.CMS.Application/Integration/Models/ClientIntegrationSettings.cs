using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Integration.Contracts.Constants;
using System;

namespace Moj.CMS.Application.Integration.Models
{
    public class ClientIntegrationSettings : Entity
    {
        public string ClientId { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public string AuthToken { get; set; }
        public DateTime TokenExpiresAt { get; set; }
    }
}
