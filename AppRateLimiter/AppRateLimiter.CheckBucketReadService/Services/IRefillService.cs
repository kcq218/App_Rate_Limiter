﻿using AppRateLimiter.Models;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface IRefillService
    {
        public void RefillBucketAsync(UserBucket user);
    }
}
