using FluentValidation;
using Gazin.Portal.Data.Dtos.Inputs;

namespace Gazin.Portal.Core.Validators
{
    public class CredentialInputDtoValidator : AbstractValidator<CredentialInputDto>
    {
        public CredentialInputDtoValidator()
        {
            RuleFor(dto => dto.Email).NotEmpty()
                                     .EmailAddress();
            RuleFor(dto => dto.Token).NotEmpty();
        }
    }
}