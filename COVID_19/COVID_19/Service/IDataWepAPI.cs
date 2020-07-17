using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19
{
    public interface IDataWepAPI
    {
        Task<Models> GetTotall();

        Task<Models> GetCountry(string countryName);

        Task<Countries> GetCountriesList();
    }
}
