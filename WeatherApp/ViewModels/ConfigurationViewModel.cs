﻿using System;
using WeatherApp.Commands;

namespace WeatherApp.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
        private string apiKey;        

        public string ApiKey { 
            get => apiKey;
            set
            {
                apiKey = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<string> SaveConfigurationCommand { get; set; }


        public ConfigurationViewModel()
        {
            Name = GetType().Name;

            ApiKey = GetApiKey();

            SaveConfigurationCommand = new DelegateCommand<string>(SaveConfiguration);
        }

        private void SaveConfiguration(string obj)
        {
            /// TODO 04 : Les tâches manquantes sont dans les XAML.
            /// TODO 04a : Sauvegarder la configuration
            /// //mais ou est le TODO 03???
            /// 
            Properties.Settings.Default.apiKey = ApiKey;
            Properties.Settings.Default.Save();
        }

        private string GetApiKey()
        {
            /// TODO 05 : Retourner la configuration
            //return string.Empty;
            return Properties.Settings.Default.apiKey;
        }

    }
}
