using System;
using Newtonsoft.Json;
namespace GoCoinAPI
{
    [Serializable]
    public class CurrencyDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string symbol { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool is_crypto { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int sort_order { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int precision { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] aliases { get; set; }
    }
}
