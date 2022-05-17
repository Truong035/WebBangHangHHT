namespace WebAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long id { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Pass { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateDate { get; set; }

        public long? idRole { get; set; }

        public bool? delete { get; set; }

        public virtual Role Role { get; set; }

        public virtual Role Role1 { get; set; }
    }
}
