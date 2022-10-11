using System;
using System.IO;
using System.Threading.Tasks;
using Azure_functions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Azure_functions
{
    public static class Connect
    {
        private static readonly string iothub = Environment.GetEnvironmentVariable("IotHub");

        private static readonly RegistryManager _registryManager = RegistryManager.CreateFromConnectionString(iothub);
        [FunctionName("Connect")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "devices/connect")] HttpRequest req,
            ILogger log)
        {
	        try
	        {
		        var body = JsonConvert.DeserializeObject<DeviceRequest>(await new StreamReader(req.Body).ReadToEndAsync());
		        var device = await _registryManager.AddDeviceAsync(new Device(body.DeviceId));
		        var connectionstring =
					 $"{iothub.Split(";")[0]};DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}";

				return new OkObjectResult(connectionstring);
	        }
	        catch
	        {
		        return new BadRequestResult();
	        }
        }

    }
}
