using DotNetMulakat.App.DTOs;
using FluentValidation;

namespace DotNetMulakat.App.Validators;

public class RegisterDtoValidator:AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(p=>p.FirstName).NotEmpty().WithMessage("Ad alanı boş olamaz");
        RuleFor(p=>p.FirstName).MinimumLength(3).WithMessage("Ad alanı en az 3 karekter olmalıdır");
    }
}
