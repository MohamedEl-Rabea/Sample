using System;

namespace Moj.CMS.Application.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
