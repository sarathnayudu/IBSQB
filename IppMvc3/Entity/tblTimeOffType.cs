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
    
    public partial class tblTimeOffType
    {
        public tblTimeOffType()
        {
            this.tblEmployeeTimeOffs = new HashSet<tblEmployeeTimeOff>();
        }
    
        public System.Guid PK_TimeOffTypeID { get; set; }
        public string TimeOffDescription { get; set; }
        public string Rec_CreatedBy { get; set; }
        public Nullable<System.DateTime> Rec_CreatedDate { get; set; }
        public string Rec_ModifiedBy { get; set; }
        public Nullable<System.DateTime> Rec_ModifiedDate { get; set; }
        public Nullable<bool> ActiveFlag { get; set; }
    
        public virtual ICollection<tblEmployeeTimeOff> tblEmployeeTimeOffs { get; set; }
    }
}
