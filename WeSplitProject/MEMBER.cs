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
    
    public partial class MEMBER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEMBER()
        {
            this.TRIP_SPLIT = new HashSet<TRIP_SPLIT>();
        }
    
        public string TRIP_ID { get; set; }
        public string MEMBER_ID { get; set; }
        public string MEMBER_NAME { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string AVATAR { get; set; }
    
        public virtual TRIP TRIP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRIP_SPLIT> TRIP_SPLIT { get; set; }
    }
}
