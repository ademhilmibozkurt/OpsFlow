using System.ComponentModel.DataAnnotations;

namespace OpsFlow.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Required][Key]
        public int Id {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public bool IsDeleted {get; set;}
    }
}