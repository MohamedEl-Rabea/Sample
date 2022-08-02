using Moj.CMS.Shared.Models;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class UpdateProfileRequestDto
    {
        public UpdateProfileRequestDto()
        {
            ProfilePicture = new();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DocumentDto ProfilePicture { get; set; }
    }
}