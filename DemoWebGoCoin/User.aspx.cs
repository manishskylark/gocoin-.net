﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoCoinAPI;
using Newtonsoft.Json;
using Logger;

namespace DemoWebGoCoin
{
    public partial class User : System.Web.UI.Page
    {
        public User _user;
        public Api _api;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            getauthorizeapi();
        }

        protected void getauthorizeapi()
        {
            Client client = new Client();
            client.client_id = "895963850ade9e8f5652ecabc7e16b3d561ea22f8d8b4c4207e6784a0db3a01f";
            client.client_secret = "6b08be21178a7f76229d7cae91a33eed023b7bf5aede577f4cc7d73ec5bd6e3f";
            client.scope = "user_read_write";
            client.redirect_uri = "http://119.226.189.186:8100/DemoWebGoCoin/";
          
            Boolean b_auth = client.authorize_api();
            if (b_auth)
            {
               LitUser.Text = SerializeJson(client.api.user.self());
            }
            else
            {
                Response.Redirect("Error in getting the User Data", false);
            }
        }

        public string SerializeJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}