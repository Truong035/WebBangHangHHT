namespace WebAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role_Permisstion
    {
        public long id { get; set; }

        public long? idRole { get; set; }

        public long? idPermisstion { get; set; }

        public virtual Permisstion Permisstion { get; set; }

        public virtual Permisstion Permisstion1 { get; set; }

        public virtual Role Role { get; set; }

        public virtual Role Role1 { get; set; }
    }
}
