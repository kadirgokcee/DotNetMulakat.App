using DotNetMulakat.App.DTOs;
using FluentValidation;

namespace DotNetMulakat.App.Validators;

public class LoginRequestDtoValidator:AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(p => p.UserNameOrEmail).MinimumLength(3).WithMessage("Kullanıcı adı en az  karakter olmalıdır.");
    }
}
