using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApp.Commands;
using WeatherApp.Services;
using OpenWeatherAPI;
using System.Windows;

namespace WeatherApp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Membres
        private BaseViewModel currentViewModel;
        private List<BaseViewModel> viewModels;
        private OpenWeatherService ows;

        #endregion

        #region Propriétés
        /// <summary>
        /// Model actuellement affiché
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { 
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Commande pour changer la page à afficher
        /// </summary>
        public DelegateCommand<string> ChangePageCommand { get; set; }

        public List<BaseViewModel> ViewModels
        {
            get {
                if (viewModels == null)
                    viewModels = new List<BaseViewModel>();
                return viewModels; 
            }
        }
        #endregion

        public ApplicationViewModel()
        {
            ChangePageCommand = new DelegateCommand<string>(ChangePage);
           
            /// TODO 11 : Commenter cette ligne lorsque la configuration utilisateur fonctionne
            //var apiKey = AppConfiguration.GetValue("OWApiKey");
            //ows = new OpenWeatherService(apiKey);

            initViewModels();
        }

        #region Méthodes
        void initViewModels()
        {

            /// TemperatureViewModel setup
            var tvm = new TemperatureViewModel();

            /// TODO 09 : Indiquer qu'il n'y a aucune clé si le Settings apiKey est vide.
            /// S'il y a une valeur, instancié OpenWeatherService avec la clé
           // try
           // {

                if (string.IsNullOrEmpty(Properties.Settings.Default.apiKey))
                {                  
                    tvm.RawText="Aucune clé API dans l'application, veuillez l'ajouter dans Fichier - > Préférences";//avait mis messagebox avant... et j'ai relu...
                }
                else
                {
                    ows = new OpenWeatherService(Properties.Settings.Default.apiKey);
                    tvm.City = OpenWeatherProcessor.Instance.City;
            }
                
            tvm.SetTemperatureService(ows);
            ViewModels.Add(tvm);

            /// TODO 01 : ConfigurationViewModel Add Configuration ViewModel
            var cvm = new ConfigurationViewModel();
            ViewModels.Add(cvm);

            CurrentViewModel = ViewModels[0];
        }

        private void ChangePage(string pageName)
        {
            /// TODO 10 : Si on a changé la clé, il faudra la mettre dans le service.
            /// 
            /// Algo
            /// Si la vue actuelle est ConfigurationViewModel
            if (CurrentViewModel == ViewModels[1])
            {
                ///   Mettre la nouvelle clé dans le OpenWeatherService        
                ///   Si le service de temperature est null
                if(ows == null)
                {
                   ///     Assigner le service de température
                   ows = new OpenWeatherService(Properties.Settings.Default.apiKey); // on ne peut pas assigné le service sans la clé
                   Properties.Settings.Default.Save();
                }
                else
                {
                    ows.SetApiKey(Properties.Settings.Default.apiKey); //si existant
                    Properties.Settings.Default.Save();
                }
                ///   Rechercher le TemperatureViewModel dans la liste des ViewModels
                //https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-5.0

            }

            /// Permet de retrouver le ViewModel avec le nom indiqé
            CurrentViewModel = ViewModels.FirstOrDefault(x => x.Name == pageName);  
        }

        #endregion
    }
}
