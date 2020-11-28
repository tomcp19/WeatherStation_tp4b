using OpenWeatherAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.ViewModels;

namespace WeatherApp.Services
{
    public class OpenWeatherService : ITemperatureService
    {
        OpenWeatherProcessor owp;

        public OpenWeatherService(string apiKey)
        {
            owp = OpenWeatherProcessor.Instance;
            owp.ApiKey = apiKey;
        }
        
        public async Task<TemperatureModel> GetTempAsync()
        {

            //{
            // DateTime = DateTime.UnixEpoch.AddSeconds(temp.DateTime),
            // Temperature = temp.Main.Temperature
            // };

            //géré trop haut dans la chaine de commande. ici ca ne fait pas la différence entre la ville ou la clé
            var temp = await owp.GetCurrentWeatherAsync();
            try
            {
               
                //if(temp != null)
                //{ 
                    var result = new TemperatureModel
                    {
                        DateTime = DateTime.UnixEpoch.AddSeconds(temp.DateTime),
                        Temperature = temp.Main.Temperature
                    };

                    return result;
                //}

                //else
               // {
                    throw new ArgumentException();
                //}
            }
            catch(Exception e)
            {
                throw new ArgumentException(e.Message);
                Console.WriteLine(e.Message);
                throw e;
            }


        }

        public void SetLocation(string location)
        {
            owp.City = location;
        }

        public void SetApiKey(string apikey)
        {
            owp.ApiKey = apikey;
        }
    }
}
