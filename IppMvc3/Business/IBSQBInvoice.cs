using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace IntuitSampleMVC.Business
{
    public class IBSQBInvoice : BusinessBase
    {
        private Customer Customer { get; set; }
        private TaxCode TaxCode { get; set; }
        private Account Account { get; set; }
        private Item Item { get; set; }
        private Term Term { get; set; }
        public string InvoiceNumber { get; set; }
       

        public IBSQBInvoice()
        {           
            TaxCode = GetTaxCode();
            Account = GetAccount();
            Item = GetItem();
            Term = GetTerm();
            InvoiceNumber = "IBS" + "12345";
        }

        private Intuit.Ipp.Data.Term GetTerm()
        {
            return DataService.FindAll(new Term(), 1, 100).Where(e=>e.Name=="Net 30").FirstOrDefault();
        }

        private Intuit.Ipp.Data.Item GetItem()
        {
            List<Item> lstItem = DataService.FindAll(new Item(), 1, 100).ToList();
            return lstItem.FirstOrDefault();
        }

        private Intuit.Ipp.Data.Account GetAccount()
        {
            List<Account> lstAccount = DataService.FindAll(new Account(), 1, 100).ToList();
            return lstAccount.Where(e => e.AccountType == AccountTypeEnum.AccountsReceivable).FirstOrDefault();
        }

        private Intuit.Ipp.Data.TaxCode GetTaxCode()
        {
           List<TaxCode> lstTaxCode= DataService.FindAll(new TaxCode(), 1, 100).ToList();
           return lstTaxCode[3];
        }

        public void NewInvoice(string custid)
        {

            Customer = DataService.FindById(new Intuit.Ipp.Data.Customer { Id = custid }); 
            DataService.Add(CreateInvoice());
        }

        private Invoice CreateInvoice()
        {
            Customer customer = Customer;// FindorAdd(context, new Customer());
            TaxCode taxCode = TaxCode;// FindorAdd(context, new TaxCode());
            Account account = Account;// FindOrADDAccount(context, AccountTypeEnum.AccountsReceivable, AccountClassificationEnum.Liability);

           Invoice invoice = new Invoice();

            //DocNumber - QBO Only, otherwise use DocNumber
            //invoice.AutoDocNumber = false;
            //invoice.AutoDocNumberSpecified = true;
            invoice.DocNumber = InvoiceNumber;

            invoice.EmailStatus = EmailStatusEnum.EmailSent;
            invoice.EmailStatusSpecified = true;



            invoice.BillEmail = customer.PrimaryEmailAddr;
            

            //TxnDate
            invoice.TxnDate = DateTime.Now.Date;
            invoice.TxnDateSpecified = true;

            //PrivateNote
            invoice.PrivateNote = "This is a private note";

            //Line
            Line invoiceLine = new Line();
            //Line Description
            invoiceLine.Description = "Software services provided by our employee Sarita Paladugu at Capital One.";
            //Line Amount
            invoiceLine.Amount = Item.UnitPrice * 10;// 330m;
            invoiceLine.AmountSpecified = true;
            //Line Detail Type
            invoiceLine.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            invoiceLine.DetailTypeSpecified = true;
            //Line Sales Item Line Detail
            SalesItemLineDetail lineSalesItemLineDetail = new SalesItemLineDetail();
            //Line Sales Item Line Detail - ItemRef
            lineSalesItemLineDetail.ItemRef = new ReferenceType()
            {
                name = Item.Name,
                Value = Item.Id
            };
            //Line Sales Item Line Detail - UnitPrice
            lineSalesItemLineDetail.AnyIntuitObject = Item.UnitPrice;// 33m;
            lineSalesItemLineDetail.ItemElementName = ItemChoiceType.UnitPrice;
            //Line Sales Item Line Detail - Qty
            lineSalesItemLineDetail.Qty = 10;
            lineSalesItemLineDetail.QtySpecified = true;

           
            //Line Sales Item Line Detail - TaxCodeRef
            //For US companies, this can be 'TAX' or 'NON'
            lineSalesItemLineDetail.TaxCodeRef = new ReferenceType()
            {
                Value = "TAX"
            };
            //Line Sales Item Line Detail - ServiceDate 
            lineSalesItemLineDetail.ServiceDate = DateTime.Now.Date;
            lineSalesItemLineDetail.ServiceDateSpecified = true;
            //Assign Sales Item Line Detail to Line Item
            invoiceLine.AnyIntuitObject = lineSalesItemLineDetail;
            //Assign Line Item to Invoice
            invoice.Line = new Line[] { invoiceLine };

            //TxnTaxDetail
            TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            txnTaxDetail.TxnTaxCodeRef = new ReferenceType()
            {
                name = taxCode.Name,
                Value = taxCode.Id
            };
            Line taxLine = new Line();
            taxLine.DetailType = LineDetailTypeEnum.TaxLineDetail;
            TaxLineDetail taxLineDetail = new TaxLineDetail();

           

            // taxCode.SalesTaxRateList.
 
            //Assigning the fist Tax Rate in this Tax Code
            taxLineDetail.TaxRateRef = taxCode.SalesTaxRateList.TaxRateDetail[0].TaxRateRef;
            taxLine.AnyIntuitObject = taxLineDetail;
            txnTaxDetail.TaxLine = new Line[] { taxLine };
            invoice.TxnTaxDetail = txnTaxDetail;

            //Customer (Client)
            invoice.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };

            //Billing Address
            PhysicalAddress billAddr = new PhysicalAddress();
            billAddr.Line1 = "502 relience elegance";
            billAddr.Line2 = "Kondapur";
            billAddr.City = "Hyd";
            billAddr.CountrySubDivisionCode = "TN";
            billAddr.Country = "INDIA";
            billAddr.PostalCode = "02301";
            billAddr.Note = "Billing Address Note";
            invoice.BillAddr = billAddr;

            //Shipping Address
            PhysicalAddress shipAddr = new PhysicalAddress();
            shipAddr.Line1 = "100 Fifth Ave.";
            shipAddr.City = "Waltham";
            shipAddr.CountrySubDivisionCode = "MA";
            shipAddr.Country = "United States";
            shipAddr.PostalCode = "02452";
            shipAddr.Note = "Shipping Address Note";
            invoice.ShipAddr = shipAddr;

            
            //SalesTermRef
            invoice.SalesTermRef = new ReferenceType()
            {
                name =Term.Name,
                Value = Term.Id
            };

            //DueDate
            invoice.DueDate = DateTime.Now.AddDays(30).Date;
            invoice.DueDateSpecified = true;

            //ARAccountRef
            invoice.ARAccountRef = new ReferenceType()
            {
                name = account.Name,
                Value = account.Id
            };           


            return invoice;
        }
    }
}