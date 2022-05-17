namespace WebAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        public long id { get; set; }

        public long? product_id { get; set; }

        public int? quantity { get; set; }

        public decimal? intomoney { get; set; }

        [StringLength(200)]
        public string uid { get; set; }

        public long? IdOption { get; set; }

        public virtual Product Product { get; set; }
    }
}
