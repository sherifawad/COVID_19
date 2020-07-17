using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19
{
    public interface IRepositoryApi
    {
        Task<T> GetAsync<T>(string uri, string authToken = "");
    }
}
