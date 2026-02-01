using System.ComponentModel.DataAnnotations;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class IncidentTask : BaseEntity
    {
        [Required]
        public string Name {get; set;}
        [Required]
        public Incident Incident {get; set;}
        public IncidentTaskState TaskState {get; set;}
    }
}