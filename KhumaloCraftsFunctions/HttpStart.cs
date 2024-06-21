using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace KhumaloCraftsFunctions
{
    public static class HttpStart
    {
        [FunctionName("HttpStart")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Parse the orchestration name from the query string
            string orchestrationName = req.Query["orchestrationName"];

            if (string.IsNullOrEmpty(orchestrationName))
            {
                return new BadRequestObjectResult("Please provide an 'orchestrationName' in the query string.");
            }

            // Start the orchestration instance
            string instanceId = await starter.StartNewAsync(orchestrationName, null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return new OkObjectResult($"Started orchestration with ID = '{instanceId}'.");
        }
    }
}
