namespace WebAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoucherDetail")]
    public partial class VoucherDetail
    {
        public int Id { get; set; }

        public long? IdProduct { get; set; }

        public int? Quantity { get; set; }

        public DateTime? CreateDate { get; set; }

        public long? IdVoucher { get; set; }

        public DateTime? DateExpiation { get; set; }

        public virtual Product Product { get; set; }

        public virtual Voucher Voucher { get; set; }
    }
}
