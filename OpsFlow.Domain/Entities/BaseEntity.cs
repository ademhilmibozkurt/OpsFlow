namespace OpsFlow.Domain.Entities
{
    public abstract class BaseEntity
    {
        public string Id {get; private set;} = string.Empty;
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public bool IsDeleted {get; set;} = false;

        public BaseEntity()
        {
        }
    }
}