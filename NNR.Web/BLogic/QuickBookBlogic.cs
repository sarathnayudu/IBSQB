﻿using Intuit.Ipp.Data;
using NNR.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NNR.Web.BLogic
{
    public class QuickBookBlogic : BusinessBase
    {
        public QbUser GetQbuser(string qbEmail)
        {
            QBEntities qe = new QBEntities();
            QbUser qbusr = qe.QbUsers.Where(e => e.Email == qbEmail).FirstOrDefault();
            return qbusr;
        }

        public void SyncNow()
        {
            //sync webcustomers to qb
            //sync qb customers to web
            //sync terms
            SyncTerms();
            //sync products/services\
            SyncItem();
            //sync taxcodes
            SyncTaxCodes();
        }

        private void SyncTaxCodes()
        {
            List<TaxCode> taxcodes = GetTaxCode();

            List<Tax> ListTax = _context.Taxes.ToList();
            foreach (TaxCode itm in taxcodes)
            {
                if (ListTax.Where(e => e.TaxQBId == itm.Id).FirstOrDefault() == null)
                {
                    Tax tx = new Tax();
                    tx.Name = itm.Name;
                    tx.Description = itm.Description;
                    tx.TaxPercentage = (float)5.0;
                    tx.TaxQBId = itm.Id;

                    _context.Taxes.Add(tx);
                    _context.SaveChanges();


                }
            }
        }

        internal void DisconnectQBUser(string qbUsrEmail,string userEmail)
        {          
            QbUser qbusr = _context.QbUsers.Where(e => e.Email == qbUsrEmail).FirstOrDefault();
            if(qbusr!=null)
            {
                List<UserQbUser> Listuserqbusr = _context.UserQbUsers.Where(e => e.QbUserId == qbusr.ID).ToList();
                _context.UserQbUsers.RemoveRange(Listuserqbusr);
                _context.QbUsers.Remove(qbusr);
                _context.SaveChanges();
            }
        }

        private void SyncItem()
        {
            List<Item> productorsrv = GetItem();

            List<Product> Listproduct = _context.Products.ToList();
            foreach (Item itm in productorsrv)
            {
                if (Listproduct.Where(e => e.QBProductId == itm.Id).FirstOrDefault() == null)
                {
                    Product prodobj = new Product();
                    prodobj.Name = itm.Name;
                    prodobj.QBProductId = itm.Id;

                    _context.Products.Add(prodobj);
                    _context.SaveChanges();

                    ProductDetail prodDetail = new ProductDetail();
                    prodDetail.Description = itm.Description;
                    prodDetail.IsTaxable = itm.Taxable;
                    prodDetail.ProductId = prodobj.Id;
                    prodDetail.QTY = (float)itm.QtyOnHand;
                    prodDetail.Name = itm.Name;
                    prodDetail.Rate = (float)itm.UnitPrice;
                    prodDetail.Amount = (decimal)(prodDetail.QTY * prodDetail.Rate);
                    prodobj.ProductDetails.Add(prodDetail);
                    _context.SaveChanges();
                }
            }
        }

        public List<ServiceDetails> GetServiceDetails(int prodid)
        {
            return _context.ProductDetails.Where(e => e.ProductId == prodid).Select(x => new ServiceDetails
            {
                Id = x.Id,
                Amount = x.Amount,
                Description = x.Description,
                IsTaxable = x.IsTaxable,
                Name = x.Name,
                ProductId = x.ProductId,
                QTY = x.QTY,
                Rate = x.Rate
            }).ToList();
        }

        private void SyncTerms()
        {
            List<Term> qbTerms = GetTerm();
            List<InvoiceTermPeriod> invTerms = _context.InvoiceTermPeriods.ToList();
            foreach (Term trm in qbTerms)
            {
                if (invTerms.Where(e => e.qbId == trm.Id).FirstOrDefault() == null)
                {
                    InvoiceTermPeriod invtrmobj = new InvoiceTermPeriod();
                    invtrmobj.Name = trm.Name;
                    invtrmobj.qbId = trm.Id;
                    invtrmobj.DateFrom = DateTime.Now.Date;
                    invtrmobj.DateTo = DateTime.Now.AddMonths(1).Date;
                    _context.InvoiceTermPeriods.Add(invtrmobj);
                    _context.SaveChanges();
                }
            }


        }

        public void RegisterUserQBUser(string userName, string qbUsrEmail)
        {
            QBEntities qe = new QBEntities();
            AspNetUser usr = qe.AspNetUsers.Where(e => e.UserName == userName).FirstOrDefault();
            QbUser qbusr = qe.QbUsers.Where(e => e.Email == qbUsrEmail).FirstOrDefault();
            if (usr != null && qbusr != null)
            {
                if (usr.UserQbUsers.Count == 0)
                {
                    qe.UserQbUsers.Add(new UserQbUser
                    {
                        UserId = usr.Id,
                        QbUserId = qbusr.ID
                    });
                    qe.SaveChanges();
                }
            }
        }

        public string GetLatestUser(string qbUsrEmail)
        {
            QBEntities qe = new QBEntities();
            QbUser qbusr = qe.QbUsers.Where(e => e.Email == qbUsrEmail).FirstOrDefault();

            return qbusr != null && qbusr.UserQbUsers != null ?
                qbusr.UserQbUsers.OrderByDescending(e => e.Id).FirstOrDefault().AspNetUser.Email : string.Empty;
        }


        public QbUser GetQbUserbyUserEmail(string userEmail)
        {
            QBEntities qe = new QBEntities();
            UserQbUser usrQbUsr = qe.UserQbUsers.Where(e => e.AspNetUser.Email == userEmail).FirstOrDefault();
            QbUser qbusr = null;
            if (usrQbUsr != null)
            {
                qbusr = qe.QbUsers.Where(e => e.ID == usrQbUsr.QbUserId).FirstOrDefault();
            }
            return qbusr;
        }

        public InvoicePreferences GetCustomerPreferences(string custQBId)
        {
            InvoicePreferences invpref = new InvoicePreferences();

            Customer cust = GetCustomerById(custQBId);

            invpref.CustQBId = cust.Id;
            invpref.CustomerName = cust.FamilyName;
            invpref.BillingAdress = cust.BillAddr.Line1;
            invpref.Crew = string.Empty;
        

            invpref.DiscountType = _context.DiscountTypes.Select(e => new ComboBase
            {
                Id = e.Id,
                DisplayValue = e.Name
            }).ToList();
           
            invpref.AmountReceived = cust.TotalRevenue;

            invpref.Email = cust.PrimaryEmailAddr.Address;
            invpref.ListProduct = _context.Products.Select(e => new ComboBase
            {
                Id = e.Id,
                DisplayValue = e.Name
            }).ToList();

            invpref.ListTerms = _context.InvoiceTermPeriods.Select(e => new ComboBase
            {
                Id = e.Id,
                DisplayValue = e.Name
            }).ToList();

            invpref.Tax = _context.Taxes.Select(e => new ComboBase
            {
                Id = e.Id,
                DisplayValue = e.Name
            }).ToList();

          

            return invpref;
        }
    }
}