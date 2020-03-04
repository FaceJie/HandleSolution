using ApplicationHelper.Messages;
using ApplicationHelper.Requests;
using FluentValidation;

namespace ApplicationHelper.Validators
{
    public class UpdateIndicatorValidator : AbstractValidator<UpdateIndicatorRequest>
    {
        public UpdateIndicatorValidator()
        {
            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(nameof(IndicatorMessage.DepenedenciesMustBeProvided) + " " + IndicatorMessage.DepenedenciesMustBeProvided);
        }
    }
}
