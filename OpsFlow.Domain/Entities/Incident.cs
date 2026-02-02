using System.ComponentModel.DataAnnotations;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class Incident : BaseEntity
    {
        [Required]
        public string Title {get; set;}
        public string Description {get; set;}
        [Required]
        public IncidentPriority Priority {get; set;}
        public IncidentState State;
    }
}