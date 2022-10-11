using Microsoft.Azure.Devices.Client;
namespace Device.Console.TemperatureApp;

class Program
{
	private static readonly DeviceClient _deviceClient = DeviceClient.CreateFromConnectionString("HostName=Fredriks-IoTHub.azure-devices.net;DeviceId=fredagsappen;SharedAccessKey=LXcgFvNX9XGWXnDoGTlS7Phnn37g2Dwym6zYcOpva1A=");
	private static bool lightState;
	public static void Main()
	{
		ConfigureDeviceAsync().ConfigureAwait(false);
		System.Console.ReadKey();
	}

	public static async Task ConfigureDeviceAsync()
	{
		await _deviceClient.SetMethodHandlerAsync("GetDeviceName", GetDeviceNameAsync, null);
		var twin = _deviceClient.GetTwinAsync();
	}

	public static async Task<MethodResponse> GetDeviceNameAsync(MethodRequest methodRequest, object userContext)
	{
		lightState = !lightState;
		System.Console.WriteLine($"Method {methodRequest.Name} has been triggered. Lightstate is {lightState}");
		return new MethodResponse(200);

	}
}