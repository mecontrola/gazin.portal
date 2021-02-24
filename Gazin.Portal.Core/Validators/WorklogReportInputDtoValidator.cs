using FluentValidation;
using Gazin.Portal.Data.Dtos.Inputs;

namespace Gazin.Portal.Core.Validators
{
    public class WorklogReportInputDtoValidator : AbstractValidator<WorklogReportInputDto>
    {
        public WorklogReportInputDtoValidator()
        {
            RuleFor(dto => dto.StartDate).NotNull();
            RuleFor(dto => dto.EndDate).NotNull();
            RuleFor(dto => dto.StartDate).LessThan(dto => dto.EndDate);
        }
    }
}