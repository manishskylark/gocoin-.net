using System;
using Logger;
using Newtonsoft.Json;

namespace GoCoinAPI
{
    [Serializable]
    public class User
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string email { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string first_name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string last_name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string merchant_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string current_password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string password_confirmation { get; set; }

        RestClient restClient;     
        private ErrorManager _errLog = new ErrorManager("File");
        private string Callbackurl = null;
        private Api _api;

        public User()
        {
            
        }

        public User(Api api)
        {
            this._api = api;
        }

        /// <summary>
        /// Gets resource owner user.
        /// </summary>
        /// <returns>Resource owner user.</returns>
        public User self()
        {
            Callbackurl = "user/?access_token=" + this._api.client.token;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            User User_self = DeserializeJson(restClient.MakeRequest());
            return User_self;
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="_user">New data for the user.</param>
        /// <returns>The updated user.</returns>
        public User update(User _user)
        {

            Callbackurl = "users/" + _user.id + "?access_token=" + this._api.client.token;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.PATCH, SerializeJson(_user), Callbackurl, this._api.client.token);
            User User_update = DeserializeJson(restClient.MakeRequest());
            return User_update;
        }

        /// <summary>
        /// Gets an existing user.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>The found user.</returns>
        public User get(string id)
        {
            Callbackurl = "users/" + id + "?access_token=" + this._api.client.token;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            User User_getbyid = DeserializeJson(restClient.MakeRequest());
            return User_getbyid;
        }
       
        /// <summary>
        /// Changes user password.
        /// </summary>
        /// <param name="_user">User data with current, new and new confirmation passwords.</param>
        /// <returns>The updated user.</returns>
        public User update_password(User _user)
        {
            Callbackurl = "users/" + _user.id + "/password" + "?access_token=" + this._api.client.token;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.PATCH, SerializeJson(_user), Callbackurl, this._api.client.token);
            restClient.MakeRequest();
            return _user;
        }
        public Boolean update_password(string id, passwordrequest passwordrequest)
        {
            Callbackurl = "users/" + id + "/password" + "?access_token=" + this._api.client.token;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.PATCH, SerializeJsonpasswordrequest(passwordrequest), Callbackurl, this._api.client.token);
            restClient.MakeRequest();
            return true;
        }

         public Boolean resetPassword(string email)
          {
              string stremail = "{" + email + "}";
             Callbackurl = "/users/request_password_reset";
             restClient = new RestClient(this._api.Baseapiurl, HttpVerb.POST, stremail , Callbackurl, this._api.client.token);
            restClient.MakeRequest();
            return true;
          }

         public Boolean resetPasswordWithToken(string id, string resettoken, resetpasswordrequest passwordrequest)
        {
            Callbackurl = "users/" + id + "/reset_password/" + resettoken;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.PATCH, SerializeJsonresetpasswordrequest(passwordrequest), Callbackurl, this._api.client.token);
            restClient.MakeRequest();
            return true;
        }

         public Boolean requestConfirmationEmail(string email)
        {
            string stremail = "{" + email + "}";
            Callbackurl = "users/request_new_confirmation_email";
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.POST, stremail, Callbackurl, this._api.client.token);
            restClient.MakeRequest();
            return true;
        }

         public Boolean confirmUser(string id, string confirm_token)
        {
            Callbackurl = "users/" + id + "/confirm_account/" + confirm_token;
            restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
            restClient.MakeRequest();
            return true;
        }
        
        

        public Applications[] getUserApplications(string id)
          {
         Callbackurl = "users/" + id + "/applications";
         restClient = new RestClient(this._api.Baseapiurl, HttpVerb.GET, "", Callbackurl, this._api.client.token);
         Applications[] applications = DeserializeJsonArray(restClient.MakeRequest());
         return applications;
          }

        //Todo: Deserialize Json to type T
        private User DeserializeJson(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject<User>(jsonObjectString);
        }

        // Todo: Searialize type T to Json
        private string SerializeJson(User obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private Applications[] DeserializeJsonArray(string jsonObjectString)
        {
            return JsonConvert.DeserializeObject<SearchApplicationsResult>(jsonObjectString).applications;
        }
        private string SerializeJsonpasswordrequest(passwordrequest obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        private string SerializeJsonresetpasswordrequest(resetpasswordrequest obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        private class SearchApplicationsResult
        {
            public string status { get; set; }
            public Applications[] applications { get; set; }
        }
    }
    [Serializable]
    public class passwordrequest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string current_password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string password_confirmation { get; set; }
    }
    [Serializable]
    public class resetpasswordrequest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string password_confirmation { get; set; }
    }
  
}


