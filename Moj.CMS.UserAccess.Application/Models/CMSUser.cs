using Microsoft.AspNetCore.Identity;
using System;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.UserAccess.Application.Models
{
    public class CMSUser : IdentityUser, IFullAudited
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string DeleterUserId { get; set; }
        public Document ProfilePicture { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public CMSUser()
        {

        }
    }
}