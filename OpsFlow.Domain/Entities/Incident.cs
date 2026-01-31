using System.ComponentModel.DataAnnotations;

namespace OpsFlow.Domain.Entities
{
    public class Incident : BaseEntity
    {
        [Required]
        public string Name {get; set;}
        public string Description {get; set;}
    }
}