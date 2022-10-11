using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.MVVM.ViewModels
{
	internal class LivingRoomViewModel
	{
		public string Title { get; set; } = "Livingroom";
		public string Temperature { get; set; } = "21";
		public string TemperatureScale { get; set; } = "°C";
		public string Humidity { get; set; } = "34";
		public string HumidityScale { get; set; } = "%";
	}
}
