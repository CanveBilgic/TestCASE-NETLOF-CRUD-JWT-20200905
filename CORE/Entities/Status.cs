using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORE.Entities
{

    public class StatusBase
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ModifyDate { get; set; }

        [Column(TypeName = "int")]
        public int CreatedBy { get; set; }
        [Column(TypeName = "int")]
        public int ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
        
        public StatusBase()
        {

        }

    }
}
