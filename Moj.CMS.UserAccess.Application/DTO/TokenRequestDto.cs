using System.ComponentModel.DataAnnotations;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class TokenRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
