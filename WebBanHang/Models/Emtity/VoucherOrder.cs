namespace WebBanHang.Models.Emtity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoucherOrder")]
    public partial class VoucherOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VoucherOrder()
        {
            VoucherOrderDetails = new HashSet<VoucherOrderDetail>();
        }

        public long id { get; set; }

        [StringLength(200)]
        public string uid { get; set; }

        [StringLength(500)]
        public string adrees { get; set; }

        [StringLength(10)]
        public string telephone { get; set; }

        [StringLength(200)]
        public string email { get; set; }

        public int? status { get; set; }

        public DateTime? createdate { get; set; }

        public DateTime? datemodified { get; set; }

        public decimal? grossAmount { get; set; }

        public decimal? discountAmount { get; set; }

        public int? paymentmethods { get; set; }

        public bool? delete { get; set; }

        [StringLength(200)]
        public string approvedby { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int? Transport { get; set; }

        [StringLength(50)]
        public string note { get; set; }

        [StringLength(50)]
        public string required_note { get; set; }

        [StringLength(200)]
        public string province { get; set; }

        [StringLength(200)]
        public string district { get; set; }

        [StringLength(300)]
        public string ward { get; set; }

        [StringLength(300)]
        public string street { get; set; }

        [StringLength(300)]
        public string deliver_time { get; set; }

        public decimal? shiper { get; set; }

        public int? statusCancel { get; set; }

        public int? pointevaluation { get; set; }

        public virtual Custormer Custormer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoucherOrderDetail> VoucherOrderDetails { get; set; }
    }
}
