using Eproject_Online_floral_delivery.common.payment;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eproject_Online_floral_delivery.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PaymentWithPayPal()
        {
            //Initialize APIContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string PayerId = Request.Params["PayerID"];
                if(string.IsNullOrEmpty(PayerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "PaymentWithPayPal/PaymentWithPayPal?";
                    var Guid = Convert.ToString((new Random()).Next(1000000000));
                    var createPayment = this.CreatePayment(apiContext, baseURI + "guid=" + Guid);
                }
            } catch(Exception )
            {

            }
            return View();
        }

        private Payment CreatePayment(APIContext apiContext, string redirectURL)
        {
            return null;
        }
    }
}