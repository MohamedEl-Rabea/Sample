using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.Models;
using System;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        [Exportable(Order = 3)]
        public string UserName { get; set; }
        [Exportable(Order = 1)]
        public string FirstName { get; set; }
        [Exportable(Order = 2)]
        public string LastName { get; set; }
        [Exportable(Order = 4)]
        public string Email { get; set; }
        [Exportable(Order = 7)]
        public bool IsActive { get; set; } = true;
        [Exportable(Order = 6)]
        public bool EmailConfirmed { get; set; }
        [Exportable(Order = 5)]
        public string PhoneNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public DocumentDto ProfilePicture { get; set; }
    }
}
