namespace WebAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Carts = new HashSet<Cart>();
            Comments = new HashSet<Comment>();
            Imgs = new HashSet<Img>();
            Option_Product = new HashSet<Option_Product>();
            VoucherDetails = new HashSet<VoucherDetail>();
            VoucherOrderDetails = new HashSet<VoucherOrderDetail>();
        }

        [StringLength(500)]
        public string name { get; set; }

        public string description { get; set; }

        [StringLength(100)]
        public string brand { get; set; }

        public int? liked_count { get; set; }

        public int? cmt_count { get; set; }

        public int? historical_sold { get; set; }

        [StringLength(50)]
        public string discount { get; set; }

        public decimal? price_before_discount { get; set; }

        public decimal? price_min_before_discount { get; set; }

        public decimal? price { get; set; }

        public long Id { get; set; }

        public long? category_id { get; set; }

        [StringLength(500)]
        public string image { get; set; }

        public bool? show_free_shipping { get; set; }

        [StringLength(500)]
        public string size_chart { get; set; }

        public double? rating_star { get; set; }

        public int? stock { get; set; }

        public int? discount_stock { get; set; }

        public bool? delete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        public virtual Categorie Categorie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Img> Imgs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Option_Product> Option_Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoucherOrderDetail> VoucherOrderDetails { get; set; }
    }
}
