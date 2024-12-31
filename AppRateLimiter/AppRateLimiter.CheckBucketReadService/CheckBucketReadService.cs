using AppRateLimiter.CheckBucketReadService.Services;
using AppRateLimiter.DAL;
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
        private readonly IGetAppId _getAppId;
        private readonly IUnitofWork _unitOfWork;

        public CheckBucketReadService(ILogger<CheckBucketReadService> logger, IUnitofWork unitofWork, IReadService readService, ICheckUrlService checkUrlService, IGetAppId getAppId)
        {
            _logger = logger;
            _readService = readService;
            _checkUrlService = checkUrlService;
            _unitOfWork = unitofWork;
            _getAppId = getAppId;
        }

        [Function("read")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {

            try
            {
                _logger.LogInformation("starting");

                var appId = _getAppId.GetAppId(req);
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
