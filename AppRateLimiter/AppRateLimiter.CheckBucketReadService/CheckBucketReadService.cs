using AppRateLimiter.CheckBucketReadService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AppRateLimiter.ReadService
{
    public class CheckBucketReadService
    {
        private readonly ILogger<CheckBucketReadService> _logger;
        private readonly IReadService _readService;
        private readonly ICheckUrlService _checkUrlService;
        private readonly IGetAppUser _getAppUser;
        private readonly IRefillService _refillService;
        private readonly ICheckRateLimitService _checkRateLimitService;

        public CheckBucketReadService(ILogger<CheckBucketReadService> logger, IReadService readService, ICheckUrlService checkUrlService, IGetAppUser getAppUser, IRefillService refillService, ICheckRateLimitService rateLimitService)
        {
            _logger = logger;
            _readService = readService;
            _checkUrlService = checkUrlService;
            _getAppUser = getAppUser;
            _refillService = refillService;
            _checkRateLimitService = rateLimitService;
        }

        [Function("read")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {

            try
            {
                _logger.LogInformation("starting");

                var user = _getAppUser.GetUser(req).Result?.FirstOrDefault();

                _refillService.RefillBucketAsync(user);

                var withinRateLimit = await _checkRateLimitService.WithinRateLimit(user);

                if (!withinRateLimit)
                {
                    var result = new ObjectResult("Too many requests you have been rate limited");
                    result.StatusCode = StatusCodes.Status429TooManyRequests;
                    return result;
                }


                var url = await _checkUrlService.CheckUrl(req);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    return new OkObjectResult(await _readService.ReadAsync(url));
                }

                return new OkObjectResult("empty body request");
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred: {Exception}", e);

                return new OkObjectResult(e.ToString());
            }
        }
    }
}
