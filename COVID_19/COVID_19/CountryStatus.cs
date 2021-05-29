using System;
using System.Collections.Generic;
using System.Text;

namespace COVID_19
{

    public class AllDataSummery
    {
        public Int64 updated { get; set; }
        public Int64 cases { get; set; }
        public Int64 todayCases { get; set; }
        public Int64 deaths { get; set; }
        public Int64 todayDeaths { get; set; }
        public Int64 recovered { get; set; }
        public Int64 todayRecovered { get; set; }
        public Int64 active { get; set; }
        public Int64 critical { get; set; }
        public float casesPerOneMillion { get; set; }
        public float deathsPerOneMillion { get; set; }
        public Int64 tests { get; set; }
        public float testsPerOneMillion { get; set; }
        public Int64 population { get; set; }
        public float oneCasePerPeople { get; set; }
        public float oneDeathPerPeople { get; set; }
        public float oneTestPerPeople { get; set; }
        public float activePerOneMillion { get; set; }
        public float recoveredPerOneMillion { get; set; }
        public float criticalPerOneMillion { get; set; }
        public Int64 affectedCountries { get; set; }
    }


    public class Allcontinents
    {
        public Int64 updated { get; set; }
        public Int64 cases { get; set; }
        public Int64 todayCases { get; set; }
        public Int64 deaths { get; set; }
        public Int64 todayDeaths { get; set; }
        public Int64 recovered { get; set; }
        public Int64 todayRecovered { get; set; }
        public Int64 active { get; set; }
        public Int64 critical { get; set; }
        public float casesPerOneMillion { get; set; }
        public float deathsPerOneMillion { get; set; }
        public Int64 tests { get; set; }
        public float testsPerOneMillion { get; set; }
        public Int64 population { get; set; }
        public string continent { get; set; }
        public float activePerOneMillion { get; set; }
        public float recoveredPerOneMillion { get; set; }
        public float criticalPerOneMillion { get; set; }
        public string[] countries { get; set; }
    }


    public class CountryStatus
    {
        public Int64 updated { get; set; }
        public string country { get; set; }
        public Countryinfo countryInfo { get; set; }
        public Int64 cases { get; set; }
        public Int64 todayCases { get; set; }
        public Int64 deaths { get; set; }
        public Int64 todayDeaths { get; set; }
        public Int64 recovered { get; set; }
        public Int64 todayRecovered { get; set; }
        public Int64 active { get; set; }
        public long critical { get; set; }
        public float casesPerOneMillion { get; set; }
        public float deathsPerOneMillion { get; set; }
        public long tests { get; set; }
        public float testsPerOneMillion { get; set; }
        public long population { get; set; }
        public string continent { get; set; }
        public float oneCasePerPeople { get; set; }
        public float oneDeathPerPeople { get; set; }
        public float oneTestPerPeople { get; set; }
        public float activePerOneMillion { get; set; }
        public float recoveredPerOneMillion { get; set; }
        public float criticalPerOneMillion { get; set; }
        public string[] countries { get; set; }
    }

    public class Countryinfo
    {
        public int _id { get; set; }
        public string iso2 { get; set; }
        public string iso3 { get; set; }
        public float lat { get; set; }
        public float _long { get; set; }
        public string flag { get; set; }
    }


}
