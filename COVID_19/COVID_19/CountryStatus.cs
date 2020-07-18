using System;
using System.Collections.Generic;
using System.Text;

namespace COVID_19
{

    public class AllDataSummery
    {
        public long updated { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int todayRecovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public float casesPerOneMillion { get; set; }
        public float deathsPerOneMillion { get; set; }
        public int tests { get; set; }
        public float testsPerOneMillion { get; set; }
        public long population { get; set; }
        public float oneCasePerPeople { get; set; }
        public float oneDeathPerPeople { get; set; }
        public float oneTestPerPeople { get; set; }
        public float activePerOneMillion { get; set; }
        public float recoveredPerOneMillion { get; set; }
        public float criticalPerOneMillion { get; set; }
        public int affectedCountries { get; set; }
    }


    public class Allcontinents
    {
        public long updated { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int todayRecovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public float casesPerOneMillion { get; set; }
        public float deathsPerOneMillion { get; set; }
        public int tests { get; set; }
        public float testsPerOneMillion { get; set; }
        public long population { get; set; }
        public string continent { get; set; }
        public float activePerOneMillion { get; set; }
        public float recoveredPerOneMillion { get; set; }
        public float criticalPerOneMillion { get; set; }
        public string[] countries { get; set; }
    }


    public class CountryStatus
    {
        public long updated { get; set; }
        public string country { get; set; }
        public Countryinfo countryInfo { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int todayRecovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public float casesPerOneMillion { get; set; }
        public float deathsPerOneMillion { get; set; }
        public int tests { get; set; }
        public float testsPerOneMillion { get; set; }
        public int population { get; set; }
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
