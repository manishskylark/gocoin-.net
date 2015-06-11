using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GoCoinAPI
{
    [Serializable]
    public class Applications
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string uid { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string secret { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string redirect_uri { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string created_at { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string updated_at { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string owner_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object owner_type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool @protected { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool allow_grant_type_password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool allow_grant_type_authorization_code { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool allow_grant_type_client_credentials { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool allow_grant_type_implicit { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object[] scopes { get; set; }
    }
}
