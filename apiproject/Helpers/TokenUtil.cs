using System.Net.NetworkInformation;
using System.IO;
using System;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;

namespace Helpers
{
  public class TokenUtil
  {
    public TokenUtil()
    {
    }

    public static String getSubject(Microsoft.AspNetCore.Http.HttpRequest request)
    {
      var auth = request.Headers["Authorization"].ToString();
      if (auth.Equals("") || auth == null || auth.Length == 0 || String.IsNullOrEmpty(auth) || String.IsNullOrWhiteSpace(auth))
      {
        return null;
      }
      var jwt = auth.Substring(auth.IndexOf(" ") + 1);
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(jwt);
      var subject = token.Subject;
      return subject;
    }

  }
}