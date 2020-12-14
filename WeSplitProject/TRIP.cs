//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeSplitProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRIP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRIP()
        {
            this.EXPENSEs = new HashSet<EXPENSE>();
            this.MEMBERs = new HashSet<MEMBER>();
            this.VISIT_LOCATION = new HashSet<VISIT_LOCATION>();
        }
    
        public string TRIP_ID { get; set; }
        public string TRIP_NAME { get; set; }
        public string TRIP_DESTINATION { get; set; }
        public string TRIP_DESCRIPTION { get; set; }
        public Nullable<System.DateTime> DATE_BEGIN { get; set; }
        public Nullable<System.DateTime> DATE_FINISH { get; set; }
        public Nullable<int> TRIP_STATUS { get; set; }
        public string IMAGE_LINK { get; set; }
        public Nullable<bool> EXIST_STATUS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXPENSE> EXPENSEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER> MEMBERs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VISIT_LOCATION> VISIT_LOCATION { get; set; }
    }
}
