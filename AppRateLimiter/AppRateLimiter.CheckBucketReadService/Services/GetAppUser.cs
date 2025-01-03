﻿using AppRateLimiter.DAL;
using AppRateLimiter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class GetAppUser : IGetAppUser
    {
        private IUnitofWork unitofWork;
        private string _appid;
        private IEnumerable<UserBucket> _user;

        public GetAppUser(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }

        public async Task<IEnumerable<UserBucket>> GetUser(HttpRequest req)
        {
            var authorization = req.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var token = new JwtSecurityToken(headerValue.Parameter);
                _appid = token.Claims.First(c => c.Type == "appid").Value;
            }

            if (!string.IsNullOrEmpty(_appid))
            {
                _user = await unitofWork.UserBucketRepository.FindAsync(m => m.ClientId == _appid);
            }

            if (_user == null || !_user.Any())
            {
                var newUser = new UserBucket
                {
                    ClientId = _appid,
                    IpAddress = req.HttpContext.Connection.RemoteIpAddress.ToString(),
                    LastAccessed = DateTime.Now,
                    RefillRateSeconds = 30,
                    BucketCount = 2,
                    BucketLimit = 2,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                await unitofWork.UserBucketRepository.AddAsync(newUser);
                unitofWork.Save();
                _user = new List<UserBucket> { newUser };
            }

            return _user;
        }
    }
}
