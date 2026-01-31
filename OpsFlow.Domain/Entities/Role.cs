using System.ComponentModel.DataAnnotations;

namespace OpsFlow.Domain.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        public string Name {get; set;}
    }
}