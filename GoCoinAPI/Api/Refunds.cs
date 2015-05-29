using System;
using Newtonsoft.Json;
using Logger;

namespace GoCoinAPI
{

    [Serializable]
    public class RefundRequest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string payment_address { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float amount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string currency { get; set; }
    }

    [Serializable]
    public class Refunds
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string invoice_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string payment_address { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string amount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string currency { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string payment_currency { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string amount_in_payment_currency { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string base_to_payment_spot_rate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string payout_to_payment_spot_rate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CurrencyDetails currency_detail { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CurrencyDetails payment_currency_detail { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string spot_rate_quote_expires_at { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string created_at { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string updated_at { get; set; }

        RestClient restClient;
        private Api _api;

        public Refunds()
        {

        }

        public Refunds(Api api)
        {
            this._api = api;
        }

        private ErrorManager _errLog = new ErrorManager("File");
        private string Callbackurl = null;



        /// <summary>
        /// Create a new refund on an invoice.
        /// </summary>
        /// <param name="id">Invoice id.</param>
        /// <param name="_refundrequest">RefundRequest object.</param>
        /// <returns>The Refund data.</returns>
        public Refunds create(string id, RefundRequest _refundrequest)
        {
            Callbackurl = "invoices/" + id + "/refunds";
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.POST, SerializeJson(_refundrequest), Callbackurl, this._api.client.token);
            Refunds Invoices_refund = DeserializeJson(restClient.MakeRequest());
            return Invoices_refund;
        }

        /// <summary>
        /// Gets a list of all refunds on an invoice.
        /// </summary>
        /// <param name="id">Invoice id.</param>
        /// <returns>The array of refunds that match the invoice id.</returns>
        public Refunds[] search(string id)
        {
            Callbackurl = "invoices/" + id + "/refunds";
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            Refunds[] Invoices_refund = DeserializeJsonRefundsArray(restClient.MakeRequest());
            return Invoices_refund;
        }

        /// <summary>
        /// Get a refund.
        /// </summary>
        /// <param name="id">refund id.</param>
        /// <returns>The Refund data.</returns>
        public Refunds get(string id)
        {
            Callbackurl = "refunds/" + id;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            Refunds Invoices_refund = DeserializeJson(restClient.MakeRequest());
            return Invoices_refund;
        }

        /// <summary>
        /// Get a refund.
        /// </summary>
        /// <param name="id">refund id.</param>
        /// <returns>The Refund data.</returns>
        public void delete(string id, RefundRequest _refundrequest)
        {
            Callbackurl = "refunds/" + id;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.DELETE, "", Callbackurl, this._api.client.token);
            //Refunds Refunds_refund = DeserializeJsonRefund(restClient.MakeRequest());
            //return Refunds_refund;
        }
        /// <summary>
        /// Refresh the invoice spot rate if expired.
        /// </summary>
        /// <param name="id">refund id.</param>
        /// <returns>The refund data.</returns>
        public Refunds refresh_spot_rate(string id)
        {
            Callbackurl = "refunds/" + id + "/refresh_merchant_review_spot_rate";
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.PUT, "", Callbackurl, this._api.client.token);
            Refunds Refunds_update = DeserializeJson(restClient.MakeRequest());
            return Refunds_update;
        }


        /// <summary>
        /// Accept the quoted spot rate and initiate the refund.
        /// </summary>
        /// <param name="id">refund id.</param>
        /// <returns>The refund data.</returns>
        public Refunds accept(string id)
        {
            Callbackurl = "refunds/" + id + "/accept_spot_rate";
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.PUT, "", Callbackurl, this._api.client.token);
            Refunds Refunds_update = DeserializeJson(restClient.MakeRequest());
            return Refunds_update;
        }

        private Refunds DeserializeJson(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject<Refunds>(jsonObjectString);
        }
        private Refunds[] DeserializeJsonRefundsArray(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject<SearchRefundsResult>(jsonObjectString).refunds;
        }
        private class SearchRefundsResult
        {
            public string status { get; set; }
            public Refunds[] refunds { get; set; }
        }

        private string SerializeJson(RefundRequest obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
