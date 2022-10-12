using SmartApp.MVVM.Models;
using System.Threading.Tasks;

namespace SmartApp.Services
{
	internal interface ITemperatureService
	{
		public Task<TemperatureResponse> GetTempDataAsync(string uri = "https://api.openweathermap.org/data/2.5/weather?lat=59.1881139&lon=18.1140349&appid=c16532c5f4f00a81b0dd9b586e7232ad");
	}
}