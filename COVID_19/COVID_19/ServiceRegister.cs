using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace COVID_19
{
    public class ServiceRegister
    {
        public static void Register()
        {
            DependencyService.Register<IRepositoryApi, RepositoryApi>();
            DependencyService.Register<IDataWepAPI, DataWepAPI>();
        }
    }
}
