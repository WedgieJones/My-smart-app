using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Azure.Devices.Client;
using Microsoft.Data.SqlClient;
using Dapper;
using IOT_device.Models;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;


namespace IOT_device
{
	public partial class MainWindow : Window
	{
		private readonly string _connectURL = "http://localhost:7240/api/devices/connect";
		private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Fredrik\\OneDrive\\Dokumenter\\laptopDb.mdf;Integrated Security=True;Connect Timeout=30";
		private DeviceClient _deviceClient;
		private string _deviceId = "";
		private DeviceInfo _deviceInfo;
		private bool _connected = false;
		private bool _lightState = false;
		private bool _lightPrevState = false;
		private int _interval = 1000;
		public MainWindow()
		{
			InitializeComponent();
			SetupAsync().ConfigureAwait(false);
			SendMessageAsync().ConfigureAwait(false);
		}

		private async Task SetupAsync()
		{
			tbStateMessage.Text = "Initializing device. Please wait.";

			using IDbConnection conn = new SqlConnection(_connectionString);
			_deviceId = await conn.QueryFirstOrDefaultAsync<string>("SELECT DeviceId FROM DeviceInfo");
			if (string.IsNullOrEmpty(_deviceId))
			{
				tbStateMessage.Text = "Chill! Im generating a new device id.";
				_deviceId = Guid.NewGuid().ToString();
				await conn.ExecuteAsync("INSERT into DeviceInfo (DeviceId, DeviceName, DeviceType, Location, Owner) VALUES (@DeviceId, @DeviceName, @DeviceType, @Location, @Owner)",
					new {DeviceId = _deviceId, DeviceName = "WPF device", DeviceType = "light", Location = "kitchen", Owner="Fredrik" });
			}

			var deviceConnectionString = await conn.QueryFirstOrDefaultAsync<string>(
				"SELECT ConnectionString FROM DeviceInfo WHERE DeviceId = @DeviceId", new {DeviceId = _deviceId});
			try
			{
				await Task.Delay(5000);
				if (string.IsNullOrEmpty(deviceConnectionString))
				{
					tbStateMessage.Text = "Stay frosty. Initializing connection string.";
					using var http = new HttpClient();
					var result = await http.PostAsJsonAsync(_connectURL, new { deviceId = _deviceId });
					deviceConnectionString = await result.Content.ReadAsStringAsync();
					await conn.ExecuteAsync(
						"UPDATE DeviceInfo SET ConnectionString = @ConnectionString WHERE DeviceId = @DeviceId",
						new { DeviceId = _deviceId, ConnectionString = deviceConnectionString });

				}
			}
			catch(Exception ex){}

			

			_deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString);
			tbStateMessage.Text = "Updating twin properties. CHILL!";

			_deviceInfo =  await conn.QueryFirstOrDefaultAsync<DeviceInfo>("SELECT * FROM DeviceInfo WHERE DeviceId = @DeviceId",
				new {DeviceID = _deviceId});
			
			var twinCollection = new TwinCollection();
			twinCollection["deviceName"] = _deviceInfo.DeviceName;
			twinCollection["location"] = _deviceInfo.Location;
			twinCollection["deviceType"] = _deviceInfo.DeviceType;
			twinCollection["owner"] = _deviceInfo.Owner;
			twinCollection["lightState"] = _lightState
				;
			await _deviceClient.UpdateReportedPropertiesAsync(twinCollection);
			_connected = true;
			tbStateMessage.Text = "Connected! Told you too chill!";
			
		}

		private async Task SendMessageAsync()
		{
			while (true)
			{
				if (_connected)
				{
					if (_lightState != _lightPrevState)
					{
						_lightPrevState = _lightState;
						//d2c
						var json = JsonConvert.SerializeObject(new { lightState = _lightState });
						var message = new Message(Encoding.UTF8.GetBytes(json));
						message.Properties.Add("deviceName", _deviceInfo.DeviceName);
						message.Properties.Add("deviceType", _deviceInfo.DeviceType);
						message.Properties.Add("owner", _deviceInfo.Owner);
						message.Properties.Add("location", _deviceInfo.Location);

						await _deviceClient.SendEventAsync(message);
						tbStateMessage.Text = $"Message sent at {DateTime.Now}. ";

						//device twin
						var twinCollection = new TwinCollection();
						twinCollection["lightState"] = _lightState;
						await _deviceClient.UpdateReportedPropertiesAsync(twinCollection);
					}
				}
				
				await Task.Delay(_interval);
			}
		}

		private void BtnOnOff_OnClick(object sender, RoutedEventArgs e)
		{
			_lightState = !_lightState;

			if (_lightState)
				btnOnOff.Content = "Turn Off";
			else
				btnOnOff.Content = "Turn On";

		}
	}
}
