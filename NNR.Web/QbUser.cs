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
    
    public partial class QbUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QbUser()
        {
            this.UserQbUsers = new HashSet<UserQbUser>();
        }
    
        public int ID { get; set; }
        public string Email { get; set; }
        public string name { get; set; }
        public string Token { get; set; }
        public string Secret { get; set; }
        public string RelaimID { get; set; }
        public string DataSource { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserQbUser> UserQbUsers { get; set; }
    }
}
