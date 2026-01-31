using System.ComponentModel.DataAnnotations;

namespace OpsFlow.Domain.Entities
{
    public class IncidentTask : BaseEntity
    {
        [Required]
        public string Name {get; set;}
        [Required]
        public Incident Incident {get; set;}
    }
}