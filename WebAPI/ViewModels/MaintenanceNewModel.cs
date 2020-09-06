using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels
{
    public class MaintenanceNewModel
    {
        public int ID { get; set; } = 0;
        public string Description { get; set; }
    }
}