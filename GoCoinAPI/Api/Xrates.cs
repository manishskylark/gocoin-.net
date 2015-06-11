using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GoCoinAPI
{
    [Serializable]
public class BTC
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string USD { get; set; }
    }
     [Serializable]
    public class LTC
    {
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string USD { get; set; }
    }
     [Serializable]
    public class XDG
    {
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string USD { get; set; }
    }
     [Serializable]
    public class Prices
    {
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public BTC BTC { get; set; }
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public LTC LTC { get; set; }
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public XDG XDG { get; set; }
    }
     [Serializable]
    public class Xrates
    {
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string timestamp { get; set; }
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public Prices prices { get; set; }
    }
}

