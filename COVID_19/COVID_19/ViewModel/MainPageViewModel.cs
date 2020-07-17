using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Entry = Microcharts.Entry;

namespace COVID_19
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Properties  

        private readonly IDataWepAPI _data = DependencyService.Get<IDataWepAPI>();


        private Models _covidDetails;

        public Models CovidDetails
        {
            get { return _covidDetails; }
            set { SetProperty(ref _covidDetails, value); }
        }

        private Entry[] _chartEntries;

        public Entry[] ChartEntries
        {
            get { return _chartEntries; }
            set { SetProperty(ref _chartEntries, value); }
        }
        
        private Chart _chartData;

        public Chart ChartData
        {
            get { return _chartData; }
            set { SetProperty(ref _chartData, value); }
        }

        private string _selectedCountry;

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                SetProperty(ref _selectedCountry, value);
                Task.FromResult(GetRecoveryAndFatalityRateByCountry(SelectedCountry));
            }
        }
/*
        private Countries affectedCountries;

        public Countries AffectedCountries
        {
            get { return affectedCountries; }
            set { SetProperty(ref affectedCountries, value); }
        }

        private ObservableCollection<Country> _countriesList;

        public ObservableCollection<Country> CountriesList
        {
            get { return _countriesList; }
            set { SetProperty(ref _countriesList, value); }
        }
*/

        private ObservableCollection<string> _countriesListName;

        public ObservableCollection<string> CountriesListName
        {
            get { return _countriesListName; }
            set { SetProperty(ref _countriesListName, value); }
        }
        

        private bool _notConnected;

        public bool NotConnected
        {
            get { return _notConnected; }
            set { SetProperty(ref _notConnected, value); }
        }
        
        private string _lastUpdated;

        public string LastUpdated
        {
            get { return _lastUpdated; }
            set { SetProperty(ref _lastUpdated, value); }
        }

        #endregion

        #region Default Constructor

        public MainPageViewModel()
        {
            NotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            if (!NotConnected) {

                Task.WhenAll(GetAfftectedNumbersFromAPI(),
                    GetRecoveryAndFatalityRate());
            }
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            NotConnected = e.NetworkAccess != NetworkAccess.Internet;
            if (!NotConnected)
            {

                Task.WhenAll(GetAfftectedNumbersFromAPI(),
                    GetRecoveryAndFatalityRate());
            }
        }

        private async Task GetAfftectedNumbersFromAPI()
        {
            var models = await _data.GetTotall();
            UpdateRecoveryAndFatalityRate(models);
        }

        #endregion

        #region Methods

        private async Task GetRecoveryAndFatalityRate()
        {
            //AffectedCountries = await _data.GetCountriesList();
            //CountriesList = (await _data.GetCountriesList()).countries.ToObservableCollection();
            CountriesListName = (await _data.GetCountriesList())
                .countries.Select(X => X.name).ToObservableCollection();
        }

        private async Task GetRecoveryAndFatalityRateByCountry(string name)
        {
            var models = await _data.GetCountry(name);
            UpdateRecoveryAndFatalityRate(models);


        }

        private void UpdateRecoveryAndFatalityRate(Models models)
        {
            var Entries = new[]
            {
                 new Entry(models.confirmed.value)
                 {
                     Label = "Confirmed",
                     ValueLabel = models.confirmed.value.ToString(),
                     Color = SKColor.Parse("#2c3e50")
                 },
                 new Entry(models.recovered.value)
                 {
                     Label = "Recovered",
                     ValueLabel = models.recovered.value.ToString(),
                     Color = SKColor.Parse("#77d065")
                 },
                 new Entry(models.deaths.value)
                 {
                     Label = "Deaths",
                     ValueLabel = models.deaths.value.ToString(),
                     Color = SKColor.Parse("#b455b6")
                 }
            };

            ChartEntries = Entries;

            LastUpdated = "Last Update:  " + models.lastUpdate.ToString();

            ChartData = new RadialGaugeChart() { Entries = Entries };
        }

        #endregion
    }
}
