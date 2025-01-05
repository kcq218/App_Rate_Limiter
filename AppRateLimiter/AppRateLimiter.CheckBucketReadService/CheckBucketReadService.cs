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
        private readonly IGetAppUser _getAppUser;
        private readonly IRefillService _refillService;
        private readonly ICheckRateLimitService _checkRateLimitService;

        public CheckBucketReadService(ILogger<CheckBucketReadService> logger, IReadService readService, IGetAppUser getAppUser, IRefillService refillService, ICheckRateLimitService rateLimitService)
        {
            _logger = logger;
            _readService = readService;
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

                var user = await _getAppUser.GetUser(req);

                var userObj = user.FirstOrDefault();

                if (userObj == null)
                {
                    return new BadRequestObjectResult("User object not found");
                }

                _refillService.RefillBucket(userObj);

                var withinRateLimit = await _checkRateLimitService.WithinRateLimit(userObj);

                if (!withinRateLimit)
                {
                    var result = new ObjectResult("Too many requests you have been rate limited");
                    result.StatusCode = StatusCodes.Status429TooManyRequests;
                    return result;
                }
                else
                {
                    return new OkObjectResult(await _readService.ReadAsync(req));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred: {Exception}", e);

                return new BadRequestObjectResult(e.ToString());
            }
        }
    }
}
