using CORE.Enums;
using System;

namespace WebApi.ViewModels
{
    public class MaintenanceModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}