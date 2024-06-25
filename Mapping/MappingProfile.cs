using AutoMapper;
using DotNetMulakat.App.DTOs;
using DotNetMulakat.App.Models;

namespace DotNetMulakat.App.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDto, AppUser>();
    }
}
