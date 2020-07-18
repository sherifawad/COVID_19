using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19
{
    public interface IDataWepAPI
    {
        Task<AllDataSummery> GetTotall();

        Task<CountryStatus> GetCountry(string countryName);

        Task<Allcontinents[]> GetCountriesList();
    }
}
