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
    using System.Collections.Generic;
    
    public partial class tblEmployeeTimeOff
    {
        public System.Guid PK_EmpTimeOffID { get; set; }
        public Nullable<System.Guid> FK_Org_EmpID { get; set; }
        public Nullable<System.Guid> FK_TimeOffTypeID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Approved_Status { get; set; }
        public string Rec_CreatedBy { get; set; }
        public Nullable<System.DateTime> Rec_CreatedDate { get; set; }
        public string Rec_ModifiedBy { get; set; }
        public Nullable<System.DateTime> Rec_ModifiedDate { get; set; }
        public string Comments { get; set; }
    
        public virtual tblOrgEmployee tblOrgEmployee { get; set; }
        public virtual tblTimeOffType tblTimeOffType { get; set; }
    }
}
