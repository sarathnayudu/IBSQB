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
    
    public partial class sp_GetHolidays_Result
    {
        public System.Guid PK_HolidaysID { get; set; }
        public string Holidays_Name { get; set; }
        public string Holidays_Description { get; set; }
        public Nullable<System.DateTime> Holidays_Date { get; set; }
        public string Rec_CreatedBy { get; set; }
        public Nullable<System.DateTime> Rec_CreatedDate { get; set; }
        public string Rec_ModifiedBy { get; set; }
        public Nullable<System.DateTime> Rec_ModifiedDate { get; set; }
        public Nullable<bool> ActiveFlag { get; set; }
    }
}
