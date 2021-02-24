using FluentValidation;
using FluentValidation.TestHelper;
using Gazin.Portal.Core.Tests.Mocks.Dtos.Inputs;
using Gazin.Portal.Core.Validators;
using Gazin.Portal.Data.Dtos.Inputs;
using Xunit;

namespace Gazin.Portal.Core.Tests.Validators
{
    public class RefreshTokenInputDtoValidatorTests
    {
        private readonly IValidator<RefreshTokenInputDto> validator;

        public RefreshTokenInputDtoValidatorTests()
        {
            validator = new RefreshTokenInputDtoValidator();
        }

        [Fact(DisplayName = "[RefreshTokenInputDtoValidator.Validate] Deve validar todos quando estiverem preenchidos corretamente.")]
        public void DeveValidarQuandoCampoCorretos()
        {
            var model = RefreshTokenInputDtoMock.Create();
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.AccessToken);
            result.ShouldNotHaveValidationErrorFor(model => model.RefreshToken);
        }

        [Fact(DisplayName = "[RefreshTokenInputDtoValidator.Validate] Deve invalidar todos os campos quando vazio.")]
        public void DeveInvalidarQuandoCamposVazios()
        {
            var model = RefreshTokenInputDtoMock.CreateEmpty();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.AccessToken);
            result.ShouldHaveValidationErrorFor(model => model.RefreshToken);
        }
    }
}