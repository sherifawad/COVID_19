using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace COVID_19
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        #region Properties  

        private readonly IDataWepAPI _data = DependencyService.Get<IDataWepAPI>();

        public Models CovidDetails { get; set; }
        public ChartEntry[] ChartEntries { get; set; }
        public Chart ChartData { get; set; }

        public Models SelectedModel { get; set; }
        public bool IsToday { get; set; }
        public bool IsAll { get; set; }

        private string _selectedCountry;

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set { _selectedCountry = value;
                 GetByCountry(value);
                }
        }


        //public string SelectedCountry { get; set; }

        public bool NotConnected { get; set; }
        public bool NotEmptyList { get; set; }
        public string LastUpdated { get; set; }
        public ICommand TodayStatCommand { get; set; }
        public ICommand AllStatCommand { get; set; }
        public ObservableCollection<string> CountriesListName { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

/*

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
*//*
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
*//*

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
                

        private bool _notEmptyList;

        public bool NotEmptyList
        {
            get { return _notEmptyList; }
            set { SetProperty(ref _notEmptyList, value); }
        }
        
        private string _lastUpdated;

        

        public string LastUpdated
        {
            get { return _lastUpdated; }
            set { SetProperty(ref _lastUpdated, value); }
        }
*/
        #endregion

        #region Default Constructor

        public MainPageViewModel()
        {
            NotEmptyList = false;

            TodayStatCommand = new Command(async () => await ToDayAsync());
            AllStatCommand = new Command(async () => await GetTotalAsync());

            NotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            if (!NotConnected) {
                //Task.FromResult(GetAll());

                Task.WhenAll(
                    GetAll(),
                    GetTotalAsync(),
                    GetRecoveryAndFatalityRate()
                    );
            }
            else
                NotEmptyList = false;
        }

        private async Task GetTotalAsync()
        {
            if (SelectedCountry == null)
                await GetAll(false);
            else
                await GetByCountry(SelectedCountry, false);
        }

        private async Task ToDayAsync()
        {
            if (SelectedCountry == null)
                await GetAll(true);
            else
                await GetByCountry(SelectedCountry, true);
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            NotConnected = e.NetworkAccess != NetworkAccess.Internet;
            if (!NotConnected)
            {
                //Task.FromResult(GetAll());

                Task.WhenAll(GetAll(),
                    GetRecoveryAndFatalityRate());
            }
            else
                NotEmptyList = false;
        }



        #endregion

        #region Methods


        private async Task GetAll(bool toDay = true)
        {
            IsToday = toDay;
            IsAll = !toDay;
            var data = new Models();

            var status = await _data.GetTotall();
            

            if (toDay)
            {
                data = new Models
                {
                    confirmed = { value = status.todayCases },
                    deaths = { value = status.todayDeaths },
                    recovered = { value = status.todayRecovered },
                    lastUpdate = GetDateFormLong(status.updated)

                };
            }
            else
            {
                data = new Models
                {
                    confirmed = { value = status.cases },
                    deaths = { value = status.deaths },
                    recovered = { value = status.recovered },
                    lastUpdate = GetDateFormLong(status.updated)

                };
            }
            UpdateRecoveryAndFatalityRate(data);
        }

        private async Task GetByCountry(string countryName, bool toDay = true)
        {
            IsToday = toDay;
            IsAll = !toDay; 
            var data = new Models();

            var status = await _data.GetCountry(countryName);
            if(toDay)
            {
                data = new Models
                {
                    confirmed = { value = status.todayCases },
                    deaths = { value = status.todayDeaths },
                    recovered = { value = status.todayRecovered },
                    lastUpdate = GetDateFormLong(status.updated)

                };
            }
            else { 
                data = new Models
                {
                    confirmed = { value = status.cases },
                    deaths = { value = status.deaths },
                    recovered = { value = status.recovered },
                    lastUpdate = GetDateFormLong(status.updated)

                };
            }
            UpdateRecoveryAndFatalityRate(data);
        }
        

        private async Task GetRecoveryAndFatalityRate()
        {
             var list= (await _data.GetCountriesList())
                .SelectMany(x => x.countries).OrderBy(q => q).ToList();

            CountriesListName = list.ToObservableCollection();
            if (CountriesListName.Count > 0)
                NotEmptyList = true;
        }

        private void UpdateRecoveryAndFatalityRate(Models models)
        {
            SelectedModel = models;

            var chartEntries = new[]
            {
                 new ChartEntry(models.deaths.value)
                 {
                     Label = "Deaths",
                     ValueLabel = models.deaths.value.ToString(),
                     Color = SKColor.Parse("#b455b6"),
                     TextColor = SKColor.Parse("#b455b6"),
                     ValueLabelColor = SKColor.Parse("#b455b6")
                 },
                 new ChartEntry(models.recovered.value)
                 {
                     Label = "Recovered",
                     ValueLabel = models.recovered.value.ToString(),
                     Color = SKColor.Parse("#77d065"),
                     TextColor = SKColor.Parse("#77d065"),
                     ValueLabelColor = SKColor.Parse("#77d065")
                 },
                 new ChartEntry(models.confirmed.value)
                 {
                     Label = "Confirmed",
                     ValueLabel = models.confirmed.value.ToString(),
                     Color = SKColor.Parse(Color.Orange.ToHex()),
                     TextColor = SKColor.Parse(Color.Orange.ToHex()),
                     ValueLabelColor = SKColor.Parse(Color.Orange.ToHex())
                 }
            };

            ChartEntries = chartEntries;

            LastUpdated =  models.lastUpdate.ToString();

            ChartData = new RadialGaugeChart() { Entries = chartEntries };
        }

        private DateTime GetDateFormLong(long time)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            return start.AddMilliseconds(time).ToLocalTime();
        }

        #endregion
    }
}
