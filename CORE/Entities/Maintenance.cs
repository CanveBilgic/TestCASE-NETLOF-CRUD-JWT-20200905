using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORE.Entities
{
    public class Maintenance
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }

        public User User { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Description { get; set; }
        public virtual StatusBase Status { get; set; }

        public Maintenance()
        {
            Status = new StatusBase();
        }

    }
}
