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
    using System.Web.Mvc;

    public partial class DoNoiThat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoNoiThat()
        {
            this.ChiTietDonHangBans = new HashSet<ChiTietDonHangBan>();
            this.ChiTietHoaDonNhaps = new HashSet<ChiTietHoaDonNhap>();
        }
    
        public int MaDNT { get; set; }
        public string TenDNT { get; set; }
        public int MaNCC { get; set; }
        public int MaLDNT { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public string Kichco { get; set; }
        [AllowHtml]
        public string MoTa { get; set; }
        public Nullable<int> soluong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHangBan> ChiTietDonHangBans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
        public virtual LoaiDoNoiThat LoaiDoNoiThat { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
