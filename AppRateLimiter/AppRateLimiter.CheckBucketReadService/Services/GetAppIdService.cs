using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class GetAppIdService : IGetAppId
    {
        public string GetAppId(HttpRequest req)
        {
            var authorization = req.Headers[HeaderNames.Authorization];

            var appId = "";

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var token = new JwtSecurityToken(headerValue.Parameter);
                appId = token.Claims.First(c => c.Type == "appid").Value;
            }

            return appId;
        }
    }
}
