//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NNR.Web
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tax
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tax()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public int Id { get; set; }
        public string TaxContry { get; set; }
        public string TaxState { get; set; }
        public string TaxCounty { get; set; }
        public float TaxPercentage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}