//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoaiDoNoiThat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiDoNoiThat()
        {
            this.DoNoiThats = new HashSet<DoNoiThat>();
        }
    
        public int MaLDNT { get; set; }
        public string TenLDNT { get; set; }
        public string MoTa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoNoiThat> DoNoiThats { get; set; }
    }
}
