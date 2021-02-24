using FluentValidation;
using Gazin.Portal.Data.Dtos.Inputs;

namespace Gazin.Portal.Core.Validators
{
    public class IssueWorklogInputDtoValidator : AbstractValidator<IssueWorklogInputDto>
    {
        public IssueWorklogInputDtoValidator()
        {
            RuleFor(dto => dto.Issue).NotNull().NotEmpty();
            RuleFor(dto => dto.Date).NotNull();
            RuleFor(dto => dto.StartTime).NotNull();
            RuleFor(dto => dto.EndTime).NotNull();
            RuleFor(dto => dto.StartTime).LessThan(dto => dto.EndTime);
        }
    }
}