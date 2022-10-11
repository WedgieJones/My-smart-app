using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lektion12.Models
{
	internal class SaveDateModel
	{
		[JsonProperty ("deviceId")]
		public string DeviceId { get; set; } = "";
		
		[JsonProperty("deviceName")]
		public string DeviceName { get;set; } = "";

		[JsonProperty("deviceType")]
		public string DeviceType { get; set; } = "";

		[JsonProperty("location")]
		public string Location { get; set; } = "";

		[JsonProperty("owner")]
		public string Owner { get; set; } = "";

		[JsonProperty("data")]
		public dynamic Data { get; set; }
	}
}
