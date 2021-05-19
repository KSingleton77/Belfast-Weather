using System.Collections.Generic;

namespace KSingletonWeather.Models
{
    //Belfast WOEID = 44544
    /// <summary>
    /// Consolidated Weather Class with variables same name as API call result
    /// </summary>
    public class ConsolidatedWeather
    {
        public object ID { get; set; }
        public string Applicable_date { get; set; }
        public string Weather_state_name { get; set; }
        public string Weather_state_abbr { get; set; }    
        public double Min_temp { get; set; }
        public double Max_temp { get; set; }
        public double The_temp { get; set; }       

    }

    public class ResultViewModel
    {
        public List<ConsolidatedWeather> Consolidated_weather { get; set; }
        public string Title { get; set; }
    }

}
