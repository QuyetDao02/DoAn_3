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
    
    public partial class ChiTietDonHangBan
    {
        public int MaDHB { get; set; }
        public int MaDNT { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> Gia { get; set; }
    
        public virtual DonHangBan DonHangBan { get; set; }
        public virtual DoNoiThat DoNoiThat { get; set; }
    }
}
