using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORE.Entities
{
    [Table("User")]
    public class User: IdentityUser<int>
    {

        [Column(TypeName = "nvarchar(255)")]
        public string Firstname { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Lastname { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; }

        public virtual StatusBase Status { get; set; }

        // schema'da P harfi küçüktü, değiştirdim
        [Column(TypeName = "ntext")]
        public string profilePicture { get; set; }

        public ICollection<Maintenance> Maintenances { get; set; }

    }
}
