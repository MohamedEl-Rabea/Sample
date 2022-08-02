using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Services;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Users.Commands
{
    public class UpdateProfileCommand : Command<IResult>
    {
        public UpdateProfileRequestDto UpdateProfileRequestDto { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, IResult>
    {
        private readonly IDocumentManager _documentManager;
        private readonly IStringLocalizer<UsersLocalizer> _localizer;
        private readonly UserManager<CMSUser> _userManager;

        public UpdateProfileCommandHandler(IDocumentManager documentManager, IStringLocalizer<UsersLocalizer> localizer, UserManager<CMSUser> userManager)
        {
            _documentManager = documentManager;
            _localizer = localizer;
            _userManager = userManager;
        }


        public async Task<IResult> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<string>.Fail(_localizer["User Not Found."]);
            }
            user.FirstName = request.UpdateProfileRequestDto.FirstName;
            user.LastName = request.UpdateProfileRequestDto.LastName;
            user.PhoneNumber = request.UpdateProfileRequestDto.PhoneNumber;
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (request.UpdateProfileRequestDto.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, request.UpdateProfileRequestDto.PhoneNumber);
            }
            user.ProfilePicture = await _documentManager.ManageAsync(request.UpdateProfileRequestDto.ProfilePicture);
            var identityResult = await _userManager.UpdateAsync(user);
            var errors = identityResult.Errors.Select(e => e.Description).ToList();
            return identityResult.Succeeded ? Result.Success() : Result.Fail(errors);

        }
    }
}
