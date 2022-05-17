namespace WebAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voucher")]
    public partial class Voucher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voucher()
        {
            VoucherDetails = new HashSet<VoucherDetail>();
        }

        public long Id { get; set; }

        [StringLength(20)]
        public string voucher_code { get; set; }
        [DataType(DataType.Date)]
        public DateTime? start_time { get; set; }
        [DataType(DataType.Date)]
        public DateTime? end_time { get; set; }

        public decimal? discount_value { get; set; }

        public bool? Delete { get; set; }

        public int? Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
    }
}
