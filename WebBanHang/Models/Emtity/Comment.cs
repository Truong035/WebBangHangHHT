namespace WebBanHang.Models.Emtity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            Comment11 = new HashSet<Comment>();
        }

        public long Id { get; set; }

        [StringLength(200)]
        public string UseName { get; set; }

        [Column("Comment")]
        public string Comment1 { get; set; }

        public int? Start { get; set; }

        public DateTime? CreateDate { get; set; }

        public long? idProduct { get; set; }

        public long? IdReply { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment11 { get; set; }

        public virtual Comment Comment2 { get; set; }

        public virtual Product Product { get; set; }
    }
}
