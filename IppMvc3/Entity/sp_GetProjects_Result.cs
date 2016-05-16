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
    
    public partial class sp_GetProjects_Result
    {
        public System.Guid PK_Org_ProjectID { get; set; }
        public System.Guid FK_Org_EmpID { get; set; }
        public string Proj_Code { get; set; }
        public string Proj_name { get; set; }
        public string Proj_Description { get; set; }
        public System.DateTime Proj_StartDate { get; set; }
        public System.DateTime Proj_EndDate { get; set; }
        public Nullable<System.Guid> FK_Client1 { get; set; }
        public Nullable<System.Guid> FK_Client2 { get; set; }
        public Nullable<System.Guid> FK_EndClient { get; set; }
        public Nullable<decimal> BillRate { get; set; }
        public Nullable<int> BillingCycle { get; set; }
        public Nullable<bool> OverTime_Allowed { get; set; }
        public Nullable<decimal> OverTime_Rate { get; set; }
        public string Rec_CreatedBy { get; set; }
        public System.DateTime Rec_CreatedDate { get; set; }
        public string Rec_ModifiedBy { get; set; }
        public System.DateTime Rec_ModifiedDate { get; set; }
        public bool ActiveFlag { get; set; }
        public string Terms { get; set; }
        public string EmailTo { get; set; }
        public string EmailCCTo { get; set; }
        public Nullable<System.Guid> PKCustID { get; set; }
        public Nullable<System.Guid> FK_OrgID { get; set; }
        public string Cust_Name { get; set; }
        public string Cust_WebSiteURL { get; set; }
        public string Cust_Phone { get; set; }
        public string Cust_fax { get; set; }
        public string Cust_Contact_Name { get; set; }
        public string Cust_Contact_EmailAddress { get; set; }
        public string Cust_Contact_Phone { get; set; }
        public string Cust_AltContact_Name { get; set; }
        public string Cust_AltContact_EmailAddress { get; set; }
        public string Cust_AltContact_Phone { get; set; }
        public string Cust_ShipTo_Address1 { get; set; }
        public string Cust_ShipTo_Address2 { get; set; }
        public string Cust_ShipTo_City { get; set; }
        public string Cust_ShipTo_State { get; set; }
        public string Cust_ShipTo_PostalCode { get; set; }
        public string Cust_BillTo_Address1 { get; set; }
        public string Cust_BillTo_Address2 { get; set; }
        public string Cust_BillTo_City { get; set; }
        public string Cust_BillTo_State { get; set; }
        public string Cust_BillTo_PostalCode { get; set; }
        public string Rec_CreatedBy1 { get; set; }
        public Nullable<System.DateTime> Rec_CreatedDate1 { get; set; }
        public string Rec_ModifiedBy1 { get; set; }
        public Nullable<System.DateTime> Rec_ModifiedDate1 { get; set; }
        public Nullable<bool> ActiveFlag1 { get; set; }
    }
}
