using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace DotNetMulakat.App.Models;

public sealed class AppUser:IdentityUser<Guid>
{
    public string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; } = string.Empty;
}
