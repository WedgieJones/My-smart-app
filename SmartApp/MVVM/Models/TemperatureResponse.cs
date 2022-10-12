namespace SmartApp.MVVM.Models

{
	public class TemperatureResponse
    {
        private int _temperature;

        public int Temperature
        {
            get => _temperature;

            set
            {
                _temperature = (int)(value - 273.15);
            }
        }

        public int Humidity { get; set; }

        private string? _temperatureCondition;

        public string? WeatherCondition
        {
            get => _temperatureCondition;

            set
            {
                switch (value?.ToLower())
                {
                    case "clear":
                        _temperatureCondition = "\uf185";
                        break;

                    case "clouds":
                        _temperatureCondition = "\uf0c2";
                        break;

                    case "rain":
                        _temperatureCondition = "\uf73d";
                        break;

                    case "thunderstorm":
                        _temperatureCondition = "\uf76c";
                        break;

                    default:
                        _temperatureCondition = "\uf6c4";
                        break;
                }
            }
        }

    }
}