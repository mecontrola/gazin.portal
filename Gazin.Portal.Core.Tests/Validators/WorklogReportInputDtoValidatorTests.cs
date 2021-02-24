using FluentValidation;
using FluentValidation.TestHelper;
using Gazin.Portal.Core.Tests.Mocks.Dtos.Inputs;
using Gazin.Portal.Core.Validators;
using Gazin.Portal.Data.Dtos.Inputs;
using Xunit;

namespace Gazin.Portal.Core.Tests.Validators
{
    public class WorklogReportInputDtoValidatorTests
    {
        private readonly IValidator<WorklogReportInputDto> validator;

        public WorklogReportInputDtoValidatorTests()
        {
            validator = new WorklogReportInputDtoValidator();
        }

        [Fact(DisplayName = "[WorklogReportInputDtoValidator.Validate] Deve validar todos quando estiverem preenchidos corretamente.")]
        public void DeveValidarQuandoCampoCorretos()
        {
            var model = WorklogReportInputDtoMock.Create();
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.StartDate);
            result.ShouldNotHaveValidationErrorFor(model => model.EndDate);
        }

        [Fact(DisplayName = "[WorklogReportInputDtoValidator.Validate] Deve invalidar a data inicial quando for maior que a data final.")]
        public void DeveInvalidarQuandoDataInicialMaiorFinal()
        {
            var model = WorklogReportInputDtoMock.CreateStartGreaterThanEnd();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.StartDate);
            result.ShouldNotHaveValidationErrorFor(model => model.EndDate);
        }
    }
}