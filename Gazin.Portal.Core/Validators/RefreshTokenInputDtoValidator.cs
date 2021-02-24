using FluentValidation;
using Gazin.Portal.Data.Dtos.Inputs;

namespace Gazin.Portal.Core.Validators
{
    public class RefreshTokenInputDtoValidator : AbstractValidator<RefreshTokenInputDto>
    {
        public RefreshTokenInputDtoValidator()
        {
            RuleFor(dto => dto.AccessToken).NotEmpty();
            RuleFor(dto => dto.RefreshToken).NotEmpty();
        }
    }
}