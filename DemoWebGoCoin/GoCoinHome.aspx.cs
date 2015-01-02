using System;
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
    public partial class GoCoinHome : System.Web.UI.Page
    {
        Auth _auth;
        protected void Page_Load(object sender, EventArgs e)
        {
           // getauthorizeapi();

           Client client = new Client("your api key");
            var invoice1 = new Invoices
                {
                    price_currency = "BTC",
                    base_price = 134.00f,
                    base_price_currency = "USD",
                    confirmations_required = 6,
                    notification_level = "all",
                    callback_url = "https://www.example.com/gocoin/callback",
                    redirect_url = "https://www.example.com/redirect"
                };

            GoCoinAPI.Invoices inv =  client.api.invoices.create("your merchant id",invoice1);
            string token = client.getToken();
                LitAccessToken.Text = token;
             Boolean b_auth = client.authorize_api();
            if (b_auth)
            {
                GoCoinAPI.User currentuser = new GoCoinAPI.User();
                currentuser = client.api.user.self();
                Response.Write(" User Calls");
                Response.Write("</br>");
                Response.Write("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Response.Write("</br>");
                Response.Write("</br>");
                Response.Write(" User get");
                Response.Write("</br>");
                Response.Write(SerializeJson(client.api.user.get(currentuser.id)));
                Response.Write("</br>");
            }
            else
            {
                Response.Redirect("Error in getting the User Data", false);

            }
        }
    
        protected void getauthorizeapi() {
            string Redirecturl = null;
           
            Client client = new Client();
            client.client_id = "895963850ade9e8f5652ecabc7e16b3d561ea22f8d8b4c4207e6784a0db3a01f";
            client.client_secret = "6b08be21178a7f76229d7cae91a33eed023b7bf5aede577f4cc7d73ec5bd6e3f";
            client.scope = "user_read_write merchant_read_write invoice_read_write oauth_read_write";
            //client.scope = "user_read_write user_password_write";
            client.redirect_uri = "http://119.226.189.186:8100/DemoWebGoCoin/";
            Redirecturl = client.request_client(client.secure) + "://" + client.get_auth_url();
            client.initToken();
            Boolean b_auth = client.authorize_api();
            if (b_auth)
            {
                string token = client.getToken();
                LitAccessToken.Text = token;
            }
            else
            {
                Response.Redirect(Redirecturl, false);
            }
        }
       
    }
}