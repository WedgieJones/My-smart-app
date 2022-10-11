using System;
using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Logging;
using Lektion12.Models;
using Newtonsoft.Json;

namespace Lektion12
{
    public class SaveData
    {
        private static HttpClient client = new HttpClient();
        
        [FunctionName("SaveData")]
        public void Run([IoTHubTrigger("messages/events", Connection = "IotHubEndpoint")]EventData message,
	        [CosmosDB(databaseName: "NET", collectionName: "Data", CreateIfNotExists = true, ConnectionStringSetting = "CosmosDB")] out dynamic output,
            ILogger log)
        {
	        try
	        {
		        using var registryManager = RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));
		        var twin = Task.Run(() => registryManager.GetTwinAsync(message.SystemProperties["iothub-connection-device-id"].ToString())).Result;

                var data = new SaveDateModel();

                try { data.DeviceId = message.SystemProperties["iothub-connection-device-id"].ToString() ?? twin.DeviceId; }
                catch{}

                try { data.DeviceType = message.Properties["deviceType"].ToString() ?? twin.Properties.Reported["deviceType"].ToString();}
                catch{}

                try { data.DeviceName = message.Properties["deviceName"].ToString() ?? twin.Properties.Reported["deviceName"].ToString();}
                catch { }

                try { data.Location = message.Properties["location"].ToString() ?? twin.Properties.Reported["location"].ToString();}
                catch { }

                try { data.Owner = message.Properties["owner"].ToString() ?? twin.Properties.Reported["owner"].ToString();}
                catch { }

                try { data.Data = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(message.Body.Array)); }
                catch { }

                output = data;

	        }
            catch
	        {
                output = null;
	        }


            log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");
        }
    }
}