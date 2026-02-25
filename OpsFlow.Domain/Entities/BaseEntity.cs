namespace OpsFlow.Domain.Entities
{
    public abstract class BaseEntity
    {
        public string Id {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public bool IsDeleted {get; set;} = false;
    }
}