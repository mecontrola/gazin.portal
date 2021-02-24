using FluentValidation;
using FluentValidation.TestHelper;
using Gazin.Portal.Core.Tests.Mocks.Dtos.Inputs;
using Gazin.Portal.Core.Validators;
using Gazin.Portal.Data.Dtos.Inputs;
using Xunit;

namespace Gazin.Portal.Core.Tests.Validators
{
    public class CredentialInputDtoValidatorTests
    {
        private readonly IValidator<CredentialInputDto> validator;

        public CredentialInputDtoValidatorTests()
        {
            validator = new CredentialInputDtoValidator();
        }

        [Fact(DisplayName = "[CredentialInputDtoValidator.Validate] Deve validar todos quando estiverem preenchidos corretamente.")]
        public void DeveValidarQuandoCampoCorretos()
        {
            var model = CredentialInputDtoMock.Create();
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.Email);
            result.ShouldNotHaveValidationErrorFor(model => model.Token);
        }

        [Fact(DisplayName = "[CredentialInputDtoValidator.Validate] Deve invalidar o campo email quando estiver preenchido com um e-mail inválido.")]
        public void DeveInvalidarQuandoEmailIncorreto()
        {
            var model = CredentialInputDtoMock.CreateInvalidEmail();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Email);
            result.ShouldNotHaveValidationErrorFor(model => model.Token);
        }

        [Fact(DisplayName = "[CredentialInputDtoValidator.Validate] Deve invalidar todos os campos quando vazio.")]
        public void DeveInvalidarQuandoCamposVazios()
        {
            var model = CredentialInputDtoMock.CreateEmpty();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Email);
            result.ShouldHaveValidationErrorFor(model => model.Token);
        }
    }
}