using Newtonsoft.Json;
using SmartApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Services
{
	internal class TemperatureService : ITemperatureService
	{
		public async Task<TemperatureResponse> GetTempDataAsync(string uri)
		{
			try
			{
				using var client = new HttpClient();
				var response = await client.GetFromJsonAsync<TemperatureAPIResponse>(uri);
				return new TemperatureResponse
				{
					Temperature = (int)response!.main.temp,
					Humidity = response.main.humidity,
					WeatherCondition = response.weather[0].main
				};
			}
			catch { }
			return null!;


			//try
			//{
			//	using var client = new HttpClient();
			//	var response = await client.GetAsync(uri);
			//	if (!response.IsSuccessStatusCode)
			//		throw new Exception(response.StatusCode.ToString());
			//	var data  = JsonConvert.DeserializeObject<TemperatureAPIResponse>(await response.Content.ReadAsStringAsync());
			//	return new TemperatureResponse
			//	{
			//		Temperature = (int)data!.main.temp,
			//		Humidity = data.main.humidity,
			//		WeatherCondition = data.weather[0].main
			//	};
			//}
			//catch { }
			//return null!;
		}
	}
}
