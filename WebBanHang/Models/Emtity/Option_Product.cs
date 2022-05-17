namespace WebBanHang.Models.Emtity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Option_Product
    {
        public long Id { get; set; }

        public long? IdProduct { get; set; }

        [StringLength(50)]
        public string option { get; set; }

        public virtual Product Product { get; set; }
    }
}
