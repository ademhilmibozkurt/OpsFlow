using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string Name;
        public IReadOnlyCollection<string> Permissions { get; }
    }
}