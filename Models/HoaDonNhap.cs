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
    
    public partial class HoaDonNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonNhap()
        {
            this.ChiTietHoaDonNhaps = new HashSet<ChiTietHoaDonNhap>();
        }
    
        public int MaHDN { get; set; }
        public Nullable<int> MaNV { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
        public Nullable<System.DateTime> Ngaylap { get; set; }
        public Nullable<decimal> TongTien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}