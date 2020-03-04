using ApplicationHelper.Messages;
using ApplicationHelper.Requests;
using FluentValidation;

namespace ApplicationHelper.Validators
{
    public class AddIndicatorValidator : AbstractValidator<AddIndicatorRequest>
    {
        public AddIndicatorValidator()
        {
            RuleFor(x => x.IndicatorId)
                .Matches("^[a-z\\d-]+$")
                .WithMessage(nameof(IndicatorMessage.IndicatorIdHasInvalidFormat) + " " + IndicatorMessage.IndicatorIdHasInvalidFormat);

            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(nameof(IndicatorMessage.DepenedenciesMustBeProvided) + " " + IndicatorMessage.DepenedenciesMustBeProvided);
        }
    }
}
