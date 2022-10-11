using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.MVVM.ViewModels
{
	internal class BedRoomViewModel
	{
		public string Title { get; set; } = "Bedroom";
		public string Temperature { get; set; } = "18";
		public string TemperatureScale { get; set; } = "°C";
		public string Humidity { get; set; } = "31";
		public string HumidityScale { get; set; } = "%";
	}
}
