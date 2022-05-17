namespace WebBanHang.Models.Emtity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employer")]
    public partial class Employer
    {
        [Key]
        [StringLength(200)]
        public string uid { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        [StringLength(10)]
        public string telephone { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string position { get; set; }

        public bool? delete { get; set; }

        [StringLength(500)]
        public string password { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthday { get; set; }
    }
}
