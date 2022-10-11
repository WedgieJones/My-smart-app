using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lektion12
{
    public static class ConnectDevice
    {
        [FunctionName("ConnectDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "devices/connect")] HttpRequest req,
            ILogger log)
        {
	        try
	        {
		        using var registryManager = RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));
		        var device = await registryManager.GetDeviceAsync(req.Query["deviceId"]);
				if(device == null)
					device = await registryManager.AddDeviceAsync(new Device(req.Query["deviceId"]));
		        
				return new OkObjectResult($"{Environment.GetEnvironmentVariable("IotHub").Split(";")[0]};DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}");
	        }
	        catch (Exception ex)
	        {
				return new BadRequestObjectResult(ex.Message);
	        }
	        string name = req.Query["name"];

       
        }
    }
}
