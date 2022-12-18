using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DataverseFunction
{
    public class HttpDataverseFunction
    {
        private readonly ILogger _logger;
        private readonly AccountService _accountService;

        public HttpDataverseFunction(ILoggerFactory loggerFactory, AccountService accountService)
        {
            _logger = loggerFactory.CreateLogger<HttpDataverseFunction>();
            _accountService = accountService;
        }

        [Function("CreateAccount")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var accountId = new Guid();
            var errorMessage = string.Empty;

            try
            {
                var postBody = new StreamReader(req.Body).ReadToEnd();

                accountId = await _accountService.CreateAccount(postBody, _logger);

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                _logger.LogError(ex.Message);
            }
           

            var response = (errorMessage == string.Empty) ? req.CreateResponse(HttpStatusCode.OK): req.CreateResponse(HttpStatusCode.BadRequest);
            var responseResult = (errorMessage == string.Empty) ? accountId.ToString() : errorMessage;

            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            await response.WriteStringAsync(responseResult);            

            return response;
        }
    }
}
