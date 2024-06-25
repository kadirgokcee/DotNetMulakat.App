using AutoMapper;
using DotNetMulakat.App.DTOs;
using DotNetMulakat.App.Models;
using DotNetMulakat.App.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetMulakat.App.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController(UserManager<AppUser> userManager, IMapper mapper,JwtProvider jwtProvider) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto request, CancellationToken cancellationToken)
    {
        RegisterDtoValidator validator = new RegisterDtoValidator();
        ValidationResult validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        bool userNameExists = await userManager.Users.AnyAsync(p => p.UserName == request.UserName);
        if (userNameExists)
        {
            return BadRequest(new { Message = "Kullanıcı adı daha önce kayıt edildi" });
        }
        bool emailExists = await userManager.Users.AnyAsync(p => p.Email == request.Email);
        if (emailExists)
        {
            return BadRequest(new { Message = "Email adresi daha önce kayıt edilmiştir" });
        }
        AppUser user = mapper.Map<AppUser>(request);
        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            return Ok(new { Message = "Kayıt işlemi başarılıdır." });
        }
        return BadRequest(new { Message = "Kayıt esnasında bir hata ile karşılaştık" });
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken)
    {

       LoginRequestDtoValidator validator = new LoginRequestDtoValidator();
        ValidationResult validationResult=validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var user = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail);
        if (user == null)
        {
            return BadRequest(new { Message = "Kullanıcı bulunamadı" });
        }
        var result= await userManager.CheckPasswordAsync(user, request.Password);
        if (result)
        {
            return Ok(new { Message = "Kullanıcı girişi başarılıdır" ,JWT=jwtProvider.CreateToken()});
        }
        return BadRequest(new {Message= "Kullanıcı girişi başarısızdır" });

    }
}
