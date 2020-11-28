using System;
using System.Threading.Tasks;
using WeatherApp.Commands;

namespace WeatherApp.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        private TemperatureModel currentTemp;

        public ITemperatureService TemperatureService { get; private set; }

        public DelegateCommand<string> GetTempCommand { get; set; }

        public TemperatureModel CurrentTemp
        {
            get => currentTemp;
            set
            {
                currentTemp = value;
                OnPropertyChanged();
                OnPropertyChanged("RawText");
            }
        }

        private string city;


        ///  ok - TODO -- : Juste pour vous voyez la nouvelle propriété ici
        /// <summary>
        /// Ville pour effectuer la recherche
        /// </summary>
        public string City
        {
            get { return city; }
            set
            {
                city = value;

                if (TemperatureService != null)
                {
                    TemperatureService.SetLocation(City);
                }

                OnPropertyChanged();
            }
        }

        private string _rawText;

        public string RawText
        {
            get
            {
                return _rawText;
            }
            set
            {
                _rawText = value;
                OnPropertyChanged();
            }
        }

        public TemperatureViewModel()
        {
            Name = GetType().Name;

            GetTempCommand = new DelegateCommand<string>(GetTemp, CanGetTemp);
        }

        public bool CanGetTemp(string obj)
        {
            return TemperatureService != null && !String.IsNullOrEmpty(Properties.Settings.Default.apiKey);
        }

        public void GetTemp(string obj)
        {
            if (TemperatureService == null) throw new NullReferenceException();

            _ = GetTempAsync();
        }

        private async Task GetTempAsync()
        {
            CurrentTemp = await TemperatureService.GetTempAsync();

            RawText = $"Time : {CurrentTemp.DateTime.ToLocalTime()} {Environment.NewLine}Temperature : {CurrentTemp.Temperature}";
        }

        public double CelsiusInFahrenheit(double c)
        {
            return c * 9.0 / 5.0 + 32;
        }

        public double FahrenheitInCelsius(double f)
        {
            return (f - 32) * 5.0 / 9.0;
        }

        public void SetTemperatureService(ITemperatureService srv)
        {
            TemperatureService = srv;
        }
    }
}
