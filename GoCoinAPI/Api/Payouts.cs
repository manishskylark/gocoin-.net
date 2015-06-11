using System;
using Newtonsoft.Json;
using Logger;

namespace GoCoinAPI
{
    [Serializable]
    public class PayoutRequest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string payment_address { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float amount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string currency { get; set; }
    }
    [Serializable]
    public class Payouts
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string payout_account_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string currency { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string amount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string payout_account_last4 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string created_at { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string updated_at { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool verbosity { get; set; }


        RestClient restClient;
        private Api _api;

        public Payouts()
        {

        }


        public Payouts(Api api)
        {
            this._api = api;
        }

        private ErrorManager _errLog = new ErrorManager("File");
        private string Callbackurl = null;

        /// <summary>
        /// Gets a list of all payouts for a merchant.
        /// </summary>
        /// <param name="_invoiceparams">Querystring with the invoice parameters.</param>
        /// <returns>The Payouts that match the search parameters.</returns>
        public Payouts[] search(string merchantid)
        {

            Callbackurl = "merchants/" + merchantid + "/payouts";
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            Payouts[] Payouts_update = DeserializeJsonArray(restClient.MakeRequest());
            return Payouts_update;
        }

     

        /// <summary>
        /// Gets an existing merchant payout.
        /// </summary>
        /// <param name="merchantid">Merchant id.</param>
        /// <param name="payoutid">Payout id.</param>
        /// <returns>The invoice data.</returns>
        public Payouts get(string merchantid, string payoutid)
        {
            Callbackurl = "merchants/" + merchantid + "/payouts/" + payoutid + "?verbose=" + this.verbosity;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            Payouts Payouts_getbyid = DeserializeJson(restClient.MakeRequest());
            return Payouts_getbyid;
        }

        //Todo: Deserialize Json to type T
        private Payouts DeserializeJson(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject<Payouts>(jsonObjectString);
        }


        private Payouts[] DeserializeJsonArray(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject<SearchPayoutsResult>(jsonObjectString).payouts;
        }
        private class SearchPayoutsResult
        {
            public string status { get; set; }
            public Payouts[] payouts { get; set; }
        }
    }
}

