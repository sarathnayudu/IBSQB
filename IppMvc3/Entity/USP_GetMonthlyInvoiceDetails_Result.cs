//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntuitSampleMVC.Entity
{
    using System;
    
    public partial class USP_GetMonthlyInvoiceDetails_Result
    {
        public string Full_Name { get; set; }
        public string Cust_Name { get; set; }
        public Nullable<decimal> rate { get; set; }
        public Nullable<System.Guid> fk_org_empid { get; set; }
        public Nullable<int> totalhors { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Billing_Period { get; set; }
        public string TxnDate { get; set; }
        public string Inv_description { get; set; }
        public string Proj_Description { get; set; }
        public string Proj_name { get; set; }
        public string terms { get; set; }
        public System.Guid PKCustID { get; set; }
    }
}
