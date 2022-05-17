namespace WebBanHang.Models.Emtity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoucherOrderDetail")]
    public partial class VoucherOrderDetail
    {
        public long voucherId { get; set; }

        public int id { get; set; }

        public long? product_id { get; set; }

        public decimal? grossAmount { get; set; }

        public decimal? discountAmount { get; set; }

        public int? quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual VoucherOrder VoucherOrder { get; set; }
    }
}
