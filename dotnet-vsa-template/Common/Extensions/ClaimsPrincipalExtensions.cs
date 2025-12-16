using System.Security.Claims;

namespace dotnet_vsa_template.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
  private const string CLAIM_UID = "uid";
  public static Guid GetUserId(this ClaimsPrincipal? principal)
  {
    string? userId = principal?.FindFirstValue(CLAIM_UID);

    return Guid.TryParse(userId, out var parsedUserId) ? 
      parsedUserId :
      throw new ApplicationException("UserId is unavailable");
  }
}
