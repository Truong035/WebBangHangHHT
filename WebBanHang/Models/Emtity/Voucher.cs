namespace WebBanHang.Models.Emtity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voucher")]
    public partial class Voucher
    {
        public long Id { get; set; }

        [StringLength(20)]
        public string voucher_code { get; set; }

        public DateTime? start_time { get; set; }

        public DateTime? end_time { get; set; }

        public decimal? discount_value { get; set; }

        [StringLength(150)]
        public string icon_text { get; set; }

        public long? IdProduct { get; set; }

        public virtual Product Product { get; set; }
    }
}
