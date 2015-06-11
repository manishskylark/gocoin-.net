using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Logger;

namespace GoCoinAPI
{
      [Serializable]
    public class Transaction
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }
          [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string amount { get; set; }
          [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string created_at { get; set; }
          [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string commercial_document_id { get; set; }
          [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string commercial_document_type { get; set; }
          [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string commerical_document_id { get; set; }
    }
      [Serializable]
    public class PagingInfo
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int total { get; set; }
          [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int page { get; set; }
          [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int per_page { get; set; }
    }

    [Serializable]
    public class Accounts
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string currency { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string balance { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string merchantid { get; set; }

        public int status { get; set; }
        public Transaction[] transactions { get; set; }
        public PagingInfo paging_info { get; set; }
        RestClient restClient;

        private ErrorManager _errLog = new ErrorManager("File");
        private string Callbackurl = null;
        private Api _api;

        public Accounts(Api api)
        {
            this._api = api;
        }
       
        /// <summary>
        /// Gets a list of accounts and balances associated with a merchant.
        /// </summary>
        /// <param name="id">Merchant's id</param>
        /// <returns>List of accounts of the given merchant.</returns>
        public List<Accounts> alist(string id)
        {
            Callbackurl = "merchants/" + id + "/accounts"+ "?access_token=" + this._api.client.token;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            List<Accounts> Accounts_list = DeserializeListJson(restClient.MakeRequest());
            return Accounts_list;
        }


        /// <summary>
        /// Gets a list of accounts and balances associated with a merchant.
        /// </summary>
        /// <param name="id">Merchant id.</param>
        /// <returns>The array of accounts that match the merchant id.</returns>
        public Accounts[] get(string id)
        {
            Callbackurl = "merchants/" + id + "/accounts";
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            Accounts[] accounts = DeserializeJsonAccountsArray(restClient.MakeRequest());
            return accounts;
        }

        /// <summary>
        /// Get a refund.
        /// </summary>
        /// <param name="id">account id.</param>
        /// <returns>The Refund data.</returns>
        public Accounts getTransactions(string id, string searchCriteria)
        {
            Callbackurl = "accounts/" + id + "/transactions?" + searchCriteria;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            Accounts accounts = DeserializeJson(restClient.MakeRequest());
            return accounts;
        }






        //Todo: Deserialize Json to type T
        private Accounts DeserializeJson(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject<Accounts>(jsonObjectString);
        }

        //Todo: Deserialize List Json to type T
        private List<Accounts> DeserializeListJson(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject <List<Accounts>>(jsonObjectString);
        }
        private Accounts[] DeserializeJsonAccountsArray(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject<SearchAccountsResult>(jsonObjectString).accounts;
        }
        private class SearchAccountsResult
        {
            public string status { get; set; }
            public Accounts[] accounts { get; set; }
        }
        // Todo: Searialize type T to Json
        private string SerializeJson(Accounts obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        // Todo: Searialize type T to Json
        private string SerializeJson(Merchant obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}