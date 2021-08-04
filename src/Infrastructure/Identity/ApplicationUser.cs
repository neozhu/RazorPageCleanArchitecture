using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Razor.Infrastructure.Identity
{
  public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; }
    public string Site { get; set; }
    [Column(TypeName = "text")]
    public string ProfilePictureDataUrl { get; set; }
    public bool IsActive { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
  }
}
