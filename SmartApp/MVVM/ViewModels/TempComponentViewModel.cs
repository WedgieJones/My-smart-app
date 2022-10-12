using SmartApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.MVVM.ViewModels
{
	internal class TempComponentViewModel : ObservableObjects
	{

        private string? _currentTempCondition;
        private readonly ITemperatureService _tempService;

        public TempComponentViewModel(ITemperatureService tempService)
        {
            _tempService = tempService;
            SetTempAsync().ConfigureAwait(false);
        }

        public string CurrentTempCondition
        {
            get { return _currentTempCondition; }
            set
            {
                _currentTempCondition = value;
                OnPropertyChanged();
            }
        }

        private string? _currentTemperature;

        public string CurrentTemperature
        {
            get { return _currentTemperature; }
            set
            {
                _currentTemperature = value;
                OnPropertyChanged();
            }
        }

        private async Task SetTempAsync()
        {
            var temperature = await _tempService.GetTempDataAsync();
            CurrentTemperature = temperature.Temperature.ToString();
            CurrentTempCondition = temperature.WeatherCondition ?? "";
        }
    }
}
