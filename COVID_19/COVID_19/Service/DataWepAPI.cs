using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace COVID_19
{
    public class DataWepAPI : IDataWepAPI
    {
        private readonly IRepositoryApi _genericRepository = DependencyService.Get<IRepositoryApi>();
        private readonly string BaseURL = "https://covid19.mathdro.id/api/";

        public async Task<Allcontinents[]> GetCountriesList()
        {
            return await _genericRepository.GetAsync<Allcontinents[]>("https://corona.lmao.ninja/v2/continents?yesterday=true&sort=");
        }

        public async Task<CountryStatus> GetCountry(string countryName)
        {
            return await _genericRepository.GetAsync<CountryStatus>($"https://corona.lmao.ninja/v2/countries/{countryName}?yesterday=true&strict=true&query =");
        }

        public async Task<AllDataSummery> GetTotall()
        {
            return await _genericRepository.GetAsync<AllDataSummery>("https://corona.lmao.ninja/v2/all");
        }
    }
}
