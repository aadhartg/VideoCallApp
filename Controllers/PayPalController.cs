using PayPal;
using PayPal.Api.Payments;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoCallConsultant.Models;
using System.Configuration;
using VideoCallConsultant.EntityModels;
using Microsoft.AspNet.Identity;
using System.Net;
using log4net.Repository.Hierarchy;
using System.IO;
using PayPal.Exception;
using VideoCallConsultant.ViewModels;

namespace VideoCallConsultant.Controllers
{
    public class PayPalController : BaseController
    {
        // GET: /Paypal/
        public string price;
        public ActionResult Index()
        {
            return View();
        }

      
        public bool CreatePurchase(AmountViewModel purchaseInfo)
        {
            try
            {
               
                var payer = new Payer
                {
                    payment_method = "credit_card",
                    funding_instruments = new List<FundingInstrument>(),
                    payer_info = new PayerInfo
                    {
                        email = "email@example.com"
                    }
                };

                var creditCard = new CreditCard();

                if (!string.IsNullOrEmpty(purchaseInfo.CraditcardID))
                {
                    payer.funding_instruments.Add(new FundingInstrument()
                    {
                        credit_card_token = new CreditCardToken()
                        {
                            credit_card_id = purchaseInfo.CraditcardID
                        }
                    });
                }
                else
                {
                    
                }

               
               
                var transaction = new Transaction
                {
                    amount = new Amount
                    {
                        currency = "USD",
                        total = purchaseInfo.Amount.ToString()
                    },
                    description = "Featured Profile on ProductionHUB",

                };

                    var payment = new Payment()
                    {
                        intent = "sale",
                        payer = payer,
                        transactions = new List<Transaction>() { transaction }
                    };
                APIContext apiContext = PaypalConfiguration.GetAPIContext();
                var createdPayment = payment.Create(apiContext);
                    TempData["info"] = createdPayment.id;

                String action = null;
                    if (createdPayment.state == "approved")
                    {
                        action = "Completed";
                    }
                    else
                    {
                        action = "Rejected";
                    }




               

            }
            catch (Exception exc)
            {
                TempData["error"] = exc.Message;
            }

            return true;
        }






       





        //public bool PaymentWithCreditCard(AmountViewModel AmoutViewModel)
        //{
        //    //create and item for which you are taking payment
        //    //if you need to add more items in the list
        //    //Then you will need to create multiple item objects or use some loop to instantiate object
        //    Item item = new Item();
        //    item.name = "Demo Item";
        //    item.currency = "USD";
        //    item.price = "5";
        //    item.quantity = "1";
        //    item.sku = "sku";

        //    //Now make a List of Item and add the above item to it
        //    //you can create as many items as you want and add to this list
        //    List<Item> itms = new List<Item>();
        //    itms.Add(item);
        //    ItemList itemList = new ItemList();
        //    itemList.items = itms;

        //    //Address for the payment
        //    Address billingAddress = new Address();
        //    billingAddress.city = "NewYork";
        //    billingAddress.country_code = "US";
        //    billingAddress.line1 = "23rd street kew gardens";
        //    billingAddress.postal_code = "43210";
        //    billingAddress.state = "NY";
        //   // billingAddress.phone = bookingViewModel.PhoneNumber;

        //    //Now Create an object of credit card and add above details to it
        //    //Please replace your credit card details over here which you got from paypal
        //    CreditCard crdtCard = new CreditCard();
        //    crdtCard.billing_address = billingAddress;
        //    crdtCard.cvv2 =   AmoutViewModel.cvv;  //card cvv2 number
        //    crdtCard.expire_month = AmoutViewModel.ValidTillMonth; //card expire date
        //    crdtCard.expire_year = AmoutViewModel.ValidTillYear; //card expire year
        //    crdtCard.first_name = AmoutViewModel.FirstName;
        //    crdtCard.last_name = AmoutViewModel.LastName;
        //    crdtCard.number = AmoutViewModel.CraditCardNumber; //enter your credit card number here
        //    crdtCard.type = AmoutViewModel.Type; //credit card type here paypal allows 4 types
        //    // Specify details of your payment amount.
        //    Details details = new Details();
        //    details.shipping = "1";
        //    details.subtotal = "5";
        //    details.tax = "1";

        //    // Specify your total payment amount and assign the details object
        //    Amount amnt = new Amount();
        //    amnt.currency = "USD";
        //    // Total = shipping tax + subtotal.
        //    amnt.total = "7";
        //    amnt.details = details;

        //    // Now make a transaction object and assign the Amount object
        //    Transaction tran = new Transaction();
        //    tran.amount = amnt;
        //    tran.description = "Description about the payment amount.";
        //    tran.item_list = itemList;

        //    // Now, we have to make a list of transaction and add the transactions object
        //    // to this list. You can create one or more object as per your requirements

        //    List<Transaction> transactions = new List<Transaction>();
        //    transactions.Add(tran);

        //    // Now we need to specify the FundingInstrument of the Payer
        //    // for credit card payments, set the CreditCard which we made above

        //    FundingInstrument fundInstrument = new FundingInstrument();
        //    fundInstrument.credit_card = crdtCard;

        //    // The Payment creation API requires a list of FundingIntrument

        //    List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
        //    fundingInstrumentList.Add(fundInstrument);

        //    // Now create Payer object and assign the fundinginstrument list to the object
        //    Payer payr = new Payer();
        //    payr.funding_instruments = fundingInstrumentList;
        //    payr.payment_method = "credit_card";

        //    // finally create the payment object and assign the payer object & transaction list to it
        //    Payment pymnt = new Payment();
        //    pymnt.intent = "sale";
        //    pymnt.payer = payr;
        //    pymnt.transactions = transactions;

        //    try
        //    {
        //        //getting context from the paypal
        //        //basically we are sending the clientID and clientSecret key in this function
        //        //to the get the context from the paypal API to make the payment
        //        //for which we have created the object above.

        //        //Basically, apiContext object has a accesstoken which is sent by the paypal
        //        //to authenticate the payment to facilitator account.
        //        //An access token could be an alphanumeric string

        //        APIContext apiContext = PaypalConfiguration.GetAPIContext();

        //        //Create is a Payment class function which actually sends the payment details
        //        //to the paypal API for the payment. The function is passed with the ApiContext
        //        //which we received above.

        //        Payment createdPayment = pymnt.Create(apiContext);

        //        //if the createdPayment.state is "approved" it means the payment was successful else not

        //        if (createdPayment.state.ToLower() != "approved")
        //        {
        //            return false;
        //        }
        //    }
        //    catch (PayPal.PayPalException ex)
        //    {

        //        return false;
        //    }

        //     return true;
        //}

    }
}