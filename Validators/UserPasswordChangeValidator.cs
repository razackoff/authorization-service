using authorization_service.DTOs;
using FluentValidation;

namespace authorization_service.Validators;

public class UserPasswordChangeValidator : AbstractValidator<UserPasswordChangeDto>
{
    public UserPasswordChangeValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.CurrentPassword).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6); // Минимальная длина нового пароля
        RuleFor(x => x.ConfirmNewPassword).NotEmpty().Equal(x => x.NewPassword)
            .WithMessage("Confirm password must match the new password."); // Проверка совпадения нового пароля и его подтверждения
    }
}