using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Azure.Devices;
using SmartApp.MVVM.Models;

namespace SmartApp.MVVM.ViewModels
{
	internal class KitchenViewModel
	{
		private DispatcherTimer timer;
		private  ObservableCollection<DeviceItem> _deviceItems;
		private readonly RegistryManager registryManager = RegistryManager.CreateFromConnectionString("HostName=Fredriks-IoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=oTaXRPi71jD6g3fi11SX0GUcrlnMq9IeJWpPaV/utSQ=");
		private List<DeviceItem> _tempList = new List<DeviceItem>();


		public KitchenViewModel()
		{
			_deviceItems = new ObservableCollection<DeviceItem>();
			PopulateDeviceItemsAsync().ConfigureAwait(false);
			SetInterval(TimeSpan.FromSeconds(5));
		}
		
		public string Title { get; set; } = "Kitchen and Dining";
	
		public IEnumerable<DeviceItem> DeviceItems => _deviceItems;

		private void SetInterval(TimeSpan interval)
		{
			timer = new DispatcherTimer()
			{
				Interval = interval,
			};
			timer.Tick += new EventHandler(timer_tick);
			timer.Start();

		}

		private async void timer_tick(object sender, EventArgs e)
		{
			await PopulateDeviceItemsAsync();
			await UpdateDeviceItemsAsync();
		}

		private async Task UpdateDeviceItemsAsync()
		{
			_tempList.Clear();
			
			foreach (var item in _deviceItems)
			{
				var device = await registryManager.GetDeviceAsync(item.DeviceId);
				if (device == null)
					_tempList.Add(item);
			}

			foreach (var item in _tempList)
			{
				_deviceItems.Remove(item);

			}

		}

		private async Task PopulateDeviceItemsAsync()
		{
			var result = registryManager.CreateQuery("SELECT * FROM devices WHERE properties.reported.location = 'kitchen'");
			if (result.HasMoreResults)
				foreach (var twin in await result.GetNextAsTwinAsync())
				{
					var device = _deviceItems.FirstOrDefault(x => x.DeviceId == twin.DeviceId);
					if (device == null)
					{
						device = new DeviceItem()
						{
							DeviceId = twin.DeviceId
						};
						try { device.DeviceName = twin.Properties.Reported["deviceName"]; }
						catch { device.DeviceName = device.DeviceId; }
						try { device.DeviceType = twin.Properties.Reported["deviceType"]; }
						catch { }

						switch (device.DeviceType.ToLower())
						{
							case "fan":
								device.IconActive = "\uf863";
								device.IconInActive = "\uf011";
								device.StateActive = "ON";
								device.StateInActive = "OFF";
								break;
							case "light":
								device.IconActive = "\uf0eb";
								device.IconInActive = "\uf011";
								device.StateActive = "ON";
								device.StateInActive = "OFF";
								break;
							default:
								device.IconActive = "\uf2db";
								device.IconInActive = "\uf011";
								device.StateActive = "ON";
								device.StateInActive = "OFF";
								break;
						}
						_deviceItems.Add(device);
					}
					else { }

				}
			else
			{
				_deviceItems.Clear();
			}
		}





	}
}

