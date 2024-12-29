using AppRateLimiter.CheckBucketReadService.Services;
using AppRateLimiter.DAL;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace AppRateLimiter.ReadService
{
    public class CheckBucketReadService
    {
        private readonly ILogger<CheckBucketReadService> _logger;
        private readonly IReadService _readService;
        private readonly ICheckUrlService _checkUrlService;
        private readonly IUnitofWork _unitOfWork;

        public CheckBucketReadService(ILogger<CheckBucketReadService> logger, IUnitofWork unitofWork, IReadService readService, ICheckUrlService checkUrlService)
        {
            _logger = logger;
            _readService = readService;
            _checkUrlService = checkUrlService;
            _unitOfWork = unitofWork;
        }

        [Function("read")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {

            try
            {
                _logger.LogInformation("starting");

                var authorization = req.Headers[HeaderNames.Authorization];


                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;

                    // scheme will be "Bearer"
                    // parmameter will be the token itself.
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
