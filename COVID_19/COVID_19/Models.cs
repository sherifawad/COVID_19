using System;
using System.Collections.Generic;
using System.Text;

namespace COVID_19
{
    public class Models
    {
        #region Properties
        public ConfirmedCases confirmed { get; set; } = new ConfirmedCases();
        public RecoveredCases recovered { get; set; } = new RecoveredCases();
        public Deaths deaths { get; set; } = new Deaths();
        public DateTime lastUpdate { get; set; } = DateTime.Now;
        #endregion

        #region Internal classes
        public class ConfirmedCases
        {
            public long value { get; set; }
        }
        public class RecoveredCases
        {
            public long value { get; set; }
        }
        public class Deaths
        {
            public long value { get; set; }
        }

        public class DailySummary
        {
            public long value { get; set; }
        }
        #endregion
    }

    public class Country
    {
        public string name { get; set; } = string.Empty;
        public string iso2 { get; set; } = string.Empty;
        public string iso3 { get; set; } = string.Empty;
    }
    public class Countries
    {
        public List<Country> countries { get; set; } = new List<Country>();
    }
}
