using AppRateLimiter.DAL;
using AppRateLimiter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class CheckAppId : ICheckAppId
    {
        private IUnitofWork unitofWork;
        private string _appid;
        private IEnumerable<UserBucket> _user;

        public CheckAppId(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }

        public async Task<IEnumerable<UserBucket>> GetAppId(HttpRequest req)
        {
            var authorization = req.Headers[HeaderNames.Authorization];


            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var token = new JwtSecurityToken(headerValue.Parameter);
                _appid = token.Claims.First(c => c.Type == "appid").Value;
            }

            if(!string.IsNullOrEmpty(_appid))
            {
                _user = await unitofWork.UserBucketRepository.FindAsync(m => m.ClientId == _appid);
            }

            return _user;
        }
    }
}
