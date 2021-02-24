using FluentValidation;
using FluentValidation.TestHelper;
using Gazin.Portal.Core.Tests.Mocks.Dtos.Inputs;
using Gazin.Portal.Core.Validators;
using Gazin.Portal.Data.Dtos.Inputs;
using Xunit;

namespace Gazin.Portal.Core.Tests.Validators
{
    public class IssueWorklogInputDtoValidatorTests
    {
        private readonly IValidator<IssueWorklogInputDto> validator;

        public IssueWorklogInputDtoValidatorTests()
        {
            validator = new IssueWorklogInputDtoValidator();
        }

        [Fact(DisplayName = "[IssueWorklogInputDtoValidator.Validate] Deve validar todos quando estiverem preenchidos corretamente.")]
        public void DeveValidarQuandoCampoCorretos()
        {
            var model = IssueWorklogInputDtoMock.Create();
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.Issue);
            result.ShouldNotHaveValidationErrorFor(model => model.Date);
            result.ShouldNotHaveValidationErrorFor(model => model.StartTime);
            result.ShouldNotHaveValidationErrorFor(model => model.EndTime);
        }

        [Fact(DisplayName = "[IssueWorklogInputDtoValidator.Validate] Deve invalidar a issue quando o campo estiver vazio.")]
        public void DeveInvalidarQuandoIssueVazio()
        {
            var model = IssueWorklogInputDtoMock.CreateIssueEmpty();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Issue);
            result.ShouldNotHaveValidationErrorFor(model => model.Date);
            result.ShouldNotHaveValidationErrorFor(model => model.StartTime);
            result.ShouldNotHaveValidationErrorFor(model => model.EndTime);
        }

        [Fact(DisplayName = "[IssueWorklogInputDtoValidator.Validate] Deve invalidar a data inicial quando for maior que a data final.")]
        public void DeveInvalidarQuandoDataInicialMaiorFinal()
        {
            var model = IssueWorklogInputDtoMock.CreateStartGreaterThanEnd();
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.Issue);
            result.ShouldNotHaveValidationErrorFor(model => model.Date);
            result.ShouldHaveValidationErrorFor(model => model.StartTime);
            result.ShouldNotHaveValidationErrorFor(model => model.EndTime);
        }
    }
}