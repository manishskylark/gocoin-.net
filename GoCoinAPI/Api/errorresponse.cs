using System;
using Newtonsoft.Json;

namespace GoCoinAPI
{
    [Serializable]
    class ErrorResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Errors errors { get; set; }
    }
      [Serializable]
    public class Errors
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] base_price { get; set; }
    }
}
