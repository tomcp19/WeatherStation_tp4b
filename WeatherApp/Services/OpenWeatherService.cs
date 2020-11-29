﻿using OpenWeatherAPI;
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
            var temp = await owp.GetCurrentWeatherAsync();
            //error bubbling... 
            //https://social.technet.microsoft.com/wiki/contents/articles/36836.exception-bubbling-in-c.aspx
     
                var result = new TemperatureModel
                    {
                        DateTime = DateTime.UnixEpoch.AddSeconds(temp.DateTime),
                        Temperature = temp.Main.Temperature
                    };

                    return result;
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
