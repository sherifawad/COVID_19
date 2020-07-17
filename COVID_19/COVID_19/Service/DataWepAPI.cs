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

        public async Task<Countries> GetCountriesList()
        {
            return await _genericRepository.GetAsync<Countries>(BaseURL + "countries/");
        }

        public async Task<Models> GetCountry(string countryName)
        {
            return await _genericRepository.GetAsync<Models>(BaseURL + "countries/" + countryName);
        }

        public async Task<Models> GetTotall()
        {
            return await _genericRepository.GetAsync<Models>(BaseURL);
        }
    }
}
