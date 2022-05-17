namespace WebBanHang.Models.Emtity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Img")]
    public partial class Img
    {
        public long Id { get; set; }

        [StringLength(200)]
        public string Url { get; set; }

        public long? IdProduct { get; set; }

        public virtual Product Product { get; set; }
    }
}
