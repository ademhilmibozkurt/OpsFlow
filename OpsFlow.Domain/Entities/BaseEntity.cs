namespace OpsFlow.Api.Entities
{
    public class BaseEntity
    {
        public int Id;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public bool IsDeleted;
    }
}