using ApplicationHelper.Messages;
using ApplicationHelper.Requests;
using FluentValidation;

namespace ApplicationHelper.Validators
{
    public class AddWatcherValidator : AbstractValidator<AddWatcherRequest>
    {
        public AddWatcherValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(nameof(UserMessage.UserIdCannotBeEmpty) + " " + UserMessage.UserIdCannotBeEmpty);
        }
    }
}
