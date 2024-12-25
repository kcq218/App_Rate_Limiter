using AppRateLimiter.CheckBucketReadService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AppRateLimiter.ReadService
{
    public class CheckBucketReadService
    {
        private readonly ILogger<CheckBucketReadService> _logger;
        private readonly IReadService _readService;

        public CheckBucketReadService(ILogger<CheckBucketReadService> logger, IReadService readService)
        {
            _logger = logger;
            _readService = readService;
        }

        [Function("read")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {

            try
            {
                string url = req.Query["url"];

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                url = url ?? data?.url;

                if (!string.IsNullOrWhiteSpace(url))
                {
                    return new OkObjectResult(_readService.ReadAsync(url));
                }

                return new OkObjectResult("empty body request");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());

                return new OkObjectResult(e.ToString());
            }
        }
    }
}
