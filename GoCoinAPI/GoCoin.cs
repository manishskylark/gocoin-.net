using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoCoinAPI
{
    public class GoCoin
    {
         public static string VERSION = "0.7";

  //production hostnames
  public static string PRODUCTION_HOST = "api.gocoin.com";
  public static string PRODUCTION_DASHBOARD_HOST = "dashboard.gocoin.com";

  public static string TEST_HOST = "";
  public static string TEST_DASHBOARD_HOST = "";

  static private string API_MODE = "production";

/// <summary>
/// return the version of this client library
/// </summary>
/// <returns>return the version of this client library</returns>
  static public string getVersion()
  {
    return GoCoin.VERSION;
  }

  /**
   * @return the api mode
   */
  static public string getApiMode()
  {
    return GoCoin.API_MODE;
  }

  /**
   * @return set the api mode
   */
  static public string setApiMode(string mode="production")
  {
    GoCoin.API_MODE = mode;
    return GoCoin.getApiMode();
  }

  /**
   * @return a Client object
   */
  static public Client getClient(string token)
  {
   Client client = Client.getInstance(token);
    if (GoCoin.getApiMode() == "test")
    {
      client.options.Add("host",GoCoin.TEST_HOST);
      client.options.Add("dashboard_host",GoCoin.TEST_DASHBOARD_HOST);
    }
    return client;
  }

    /// <summary>
    /// return a url to redirect to request an authorization code, used later for requesting a token
    /// </summary>
    /// <param name="uclient_id"></param>
    /// <param name="uclient_secret"></param>
    /// <param name="uscope"></param>
    /// <param name="uredirect_uri"></param>
    /// <returns>return a url to redirect to request an authorization code, used later for requesting a token</returns>
        static public string requestAuthorizationCode(string uclient_id, string uclient_secret, string uscope, string uredirect_uri=null)
        {
          
          Client client = new Client{client_id=uclient_id,client_secret =uclient_secret,scope = uscope,redirect_uri =  uredirect_uri };
          return client.auth_code;
        }

  /**
   * @return an access token given a previously requested authorization code
   */
  static public string requestAccessToken(string uclient_id, string uclient_secret, string ucode, string uredirect_uri=null)
  {
    Client client = new Client{client_id=uclient_id,client_secret =uclient_secret,redirect_uri =  uredirect_uri };
    
    client.initToken();
     return client.getToken();
  }

  /**
   * @return the exchange rates
   */
  static public Xrates getExchangeRates()
  {
    return Client.getInstance().get_xrate();
  }

  //#     #   #####   #######  ######   
  //#     #  #     #  #        #     #  
  //#     #  #        #        #     #  
  //#     #   #####   #####    ######   
  //#     #        #  #        #   #    
  //#     #  #     #  #        #    #   
  // #####    #####   #######  #     #  

  /**
   * @return a user given an id, or, the current user if id is not provided
   */
  static public User getUser(string token,string id=null)
  {
    Client client = GoCoin.getClient(token);
    User user = null;
    if (string.IsNullOrEmpty(id))
    {
      user = client. api.user.self();
    }
    else
    {
      user = client.api.user.get(id);
    }
     return user;
  }

  /**
   * @return an updated user, after applying updates
   */
  static public User updateUser(string token,User user)
  {
    Client client = GoCoin.getClient(token);
    user = client.api.user.update(user);
    return user;
  }

  /**
   * @return the user appplications
   */
  static public Applications[] getUserApplications(string token,string id)
  {
    Client client = GoCoin.getClient(token);
    Applications[] apps = client.api.user.getUserApplications(id);
    return apps;
  }

  /**
   * @return a boolean if the password was successfully updated
   */
  static public Boolean updatePassword(string token,string id,passwordrequest password_request)
  {
    Client client = GoCoin.getClient(token);
    return client.api.user.update_password(id, password_request);
  }

  /**
   * @return a boolean if the reset password request went through
   */
  static public Boolean resetPassword(string token,string email)
  {
    Client client = GoCoin.getClient(token);

    return client.api.user.resetPassword(email); 
  }

  /**
   * @return a boolean if the reset password with token request went through
   */
  static public Boolean resetPasswordWithToken(string token,string id,string reset_token,resetpasswordrequest password_request)
  {
    Client client = GoCoin.getClient(token);
    return client.api.user.resetPasswordWithToken(id, reset_token, password_request);
   
  }

  /**
   * @return a boolean if the request confirmation email request went through
   */
  static public Boolean requestConfirmationEmail(string token,string email)
  {
    Client client = GoCoin.getClient(token);
    return client.api.user.requestConfirmationEmail(email);
   
  }

  /**
   * @return a boolean if the user confirm was successful
   */
  static public Boolean confirmUser(string token,string id,string confirm_token)
  {
    Client client = GoCoin.getClient(token);
    return client.api.user.confirmUser(id,confirm_token);
   
  }

  //#     #  #######  ######    #####   #     #     #     #     #  #######  
  //##   ##  #        #     #  #     #  #     #    # #    ##    #     #     
  //# # # #  #        #     #  #        #     #   #   #   # #   #     #     
  //#  #  #  #####    ######   #        #######  #     #  #  #  #     #     
  //#     #  #        #   #    #        #     #  #######  #   # #     #     
  //#     #  #        #    #   #     #  #     #  #     #  #    ##     #     
  //#     #  #######  #     #   #####   #     #  #     #  #     #     #     

  /**
   * @return a merchant given an id
   */
  static public Merchant getMerchant(string token,string id)
  {
    Client client = GoCoin.getClient(token);
    Merchant merchant = client.api.merchants.get(id);
    return merchant;
  }

  /**
   * @return an updated merchant, after applying updates
   */
  static public Merchant updateMerchant(string token,Merchant merchant)
  {
    Client client = GoCoin.getClient(token);
    Merchant merchant_updated = client.api.merchants.update(merchant);
    return merchant_updated;
  }

  ///**
  // * @return the result of requesting a payout
  // */
  //static public Payouts requestPayout(string token,string merchant_id,string amount,string currency="BTC")
  //{
  //  Client client = GoCoin.getClient(token);
  //  Payouts payout = client.api.Payout.requestPayout($merchant_id,$amount,$currency);
  //  if ($payout === FALSE) { throw new Exception($client.getError()); }
  //  else                   { return $payout; }
  //}

  ///**
  // * @return a list of merchant payouts given a payout id, or, all payouts if id is not provided
  // */
  //static public function getMerchantPayouts(string token,$merchant_id,$payout_id=NULL)
  //{
  //  Client client = GoCoin.getClient(token);
  //  $payouts = $client.api.merchant.getMerchantPayouts($merchant_id,$payout_id);
  //  if ($payouts === FALSE) { throw new Exception($client.getError()); }
  //  else                    { return $payouts; }
  //}

  ///**
  // * @return the result of requesting a currency conversion
  // */
  //static public function requestCurrencyConversion(
  //  string token,$merchant_id,$amount,$currency='BTC',$target='USD'
  //)
  //{
  //  Client client = GoCoin.getClient(token);
  //  $conversion = $client.api.merchant.requestCurrencyConversion(
  //    $merchant_id,$amount,$currency,$target
  //  );
  //  if ($conversion === FALSE)  { throw new Exception($client.getError()); }
  //  else                        { return $conversion; }
  //}

  ///**
  // * @return a list of currency conversions given a conversion id, or, all conversions if id is not provided
  // */
  //static public function getCurrencyConversions(string token,$merchant_id,$conversion_id=NULL)
  //{
  //  Client client = GoCoin.getClient(token);
  //  $conversions = $client.api.merchant.getCurrencyConversions($merchant_id,$conversion_id);
  //  if ($conversions === FALSE) { throw new Exception($client.getError()); }
  //  else                        { return $conversions; }
  //}

  //#     #  #######  ######    #####   #     #     #     #     #  #######  
  //##   ##  #        #     #  #     #  #     #    # #    ##    #     #     
  //# # # #  #        #     #  #        #     #   #   #   # #   #     #     
  //#  #  #  #####    ######   #        #######  #     #  #  #  #     #     
  //#     #  #        #   #    #        #     #  #######  #   # #     #     
  //#     #  #        #    #   #     #  #     #  #     #  #    ##     #     
  //#     #  #######  #     #   #####   #     #  #     #  #     #     #     


  //#     #   #####   #######  ######    #####   
  //#     #  #     #  #        #     #  #     #  
  //#     #  #        #        #     #  #        
  //#     #   #####   #####    ######    #####   
  //#     #        #  #        #   #          #  
  //#     #  #     #  #        #    #   #     #  
  // #####    #####   #######  #     #   #####   

  ///**
  // * @return a list of merchant users given a merchant id
  // */
  //static public User[] getMerchantUsers(string token,string merchant_id)
  //{
  //  Client client = GoCoin.getClient(token);
  //  User[] users = client.api.merchant_users.getMerchantUsers($merchant_id);
  //  if ($users === FALSE) { throw new Exception($client.getError()); }
  //  else                  { return $users; }
  //}

  //###  #     #  #     #  #######  ###   #####   #######   #####   
  // #   ##    #  #     #  #     #   #   #     #  #        #     #  
  // #   # #   #  #     #  #     #   #   #        #        #        
  // #   #  #  #  #     #  #     #   #   #        #####     #####   
  // #   #   # #   #   #   #     #   #   #        #              #  
  // #   #    ##    # #    #     #   #   #     #  #        #     #  
  //###  #     #     #     #######  ###   #####   #######   #####   

  /**
   * @return the result of creating an invoice
   */
  static public Invoices createInvoice(string token,string merchant_id,Invoices invoice)
  {
    Client client = GoCoin.getClient(token);
    Invoices invoice1 =client.api.invoices.create(merchant_id,invoice);
    return invoice1; 
  }

  /**
   * @return an invoice by id
   */
  static public Invoices getInvoice(string token,string invoice_id)
  {
    Client client = GoCoin.getClient(token);
    Invoices invoice1 = client.api.invoices.get(invoice_id);
     return invoice1; 
  }

  /**
   * @return the invoices that match the given criteria
   */
  static public Invoices[] searchInvoices(string token, string criteria = null)
  {
    Client client = GoCoin.getClient(token);
     Invoices[] invoice1 = client.api.invoices.search(criteria);
     return invoice1; 
  }

  /**
   * @ Refresh the spot rate of an invoice in Merchant Review.
   */
  static public Invoices refresh_merchant_review_spot_rate(string token, string invoice_id)
  {
      Client client = GoCoin.getClient(token);
      Invoices invoice = client.api.invoices.refresh_merchant_review_spot_rate(invoice_id);
      return invoice;
  }

  /**
   * @ Reject an invoice that is in Merchant Review.
   */
  static public Invoices cancelInvoice(string token, string invoice_id, string refundAddress)
  {
      Client client = GoCoin.getClient(token);
      Invoices invoice = client.api.invoices.cancel(invoice_id, refundAddress);
      return invoice;
  }

  /**
* @ Accept the spot rate of an invoice in Merchant Review.
*/
  static public Invoices acceptInvoice(string token, string invoice_id)
  {
      Client client = GoCoin.getClient(token);
      Invoices invoice = client.api.invoices.accept(invoice_id);
      return invoice;
  }





  //   #      #####    #####   #######  #     #  #     #  #######   #####   
  //  # #    #     #  #     #  #     #  #     #  ##    #     #     #     #  
  // #   #   #        #        #     #  #     #  # #   #     #     #        
  //#     #  #        #        #     #  #     #  #  #  #     #      #####   
  //#######  #        #        #     #  #     #  #   # #     #           #  
  //#     #  #     #  #     #  #     #  #     #  #    ##     #     #     #  
  //#     #   #####    #####   #######   #####   #     #     #      #####   

  /**
   * @return the accounts for a given merchant
   */
  static public Accounts[] getAccounts(string token,string merchant_id)
  {
    Client client = GoCoin.getClient(token);
    Accounts[] accounts = client.api.accounts.get(merchant_id);
    return accounts; 
  }

  /**
   * @return the transactions that match the given criteria
   */
  static public Accounts getAccountTransactions(string token, string account_id, string criteria = null)
  {
    Client client = GoCoin.getClient(token);
    Accounts xactions = client.api.accounts.getTransactions(account_id, criteria);
     return xactions; 
  }

//}


 //######    #######   #######  #     #  #     # ########    #####  
 //#     #   #         #        #     #  ##    #  #     #   #     # 
 //#     #   #         #        #     #  # #   #  #     #   #       
 //######    #####     #####    #     #  #  #  #  #     #    #####  
 //#   #     #         #        #     #  #   # #  #     #         # 
 //#    #    #         #        #     #  #    ##  #     #   #     # 
 //#     #   #######   #         #####   #     # ########    #####  

  static public Refunds createRefundRequest(string token, string invoice_id, RefundRequest refund_request)
  {
    Client client = GoCoin.getClient(token);
    Refunds refunds = client.api.refunds.create(invoice_id, refund_request);
    return refunds; 
  }

  static public Refunds[] searchAllRefunds(string token, string invoice_id)
  {
      Client client = GoCoin.getClient(token);
      Refunds[] refunds = client.api.refunds.search(invoice_id);
      return refunds;
  }

  static public Refunds getRefund(string token, string refund_id)
  {
      Client client = GoCoin.getClient(token);
      Refunds refunds = client.api.refunds.get(refund_id);
      return refunds;
  }
  static public Boolean deleteRefund(string token, string refund_id)
  {
      Client client = GoCoin.getClient(token);
      client.api.refunds.delete(refund_id);
      return true;
  }

  static public Refunds refreshRefundSpotRate(string token, string refund_id)
  {
      Client client = GoCoin.getClient(token);
      Refunds refund=client.api.refunds.refresh_spot_rate(refund_id);
      return refund;
  }

  static public Refunds acceptRefundIntiate(string token, string refund_id)
  {
      Client client = GoCoin.getClient(token);
      Refunds refund = client.api.refunds.accept(refund_id);
      return refund;
  }

    }
}


