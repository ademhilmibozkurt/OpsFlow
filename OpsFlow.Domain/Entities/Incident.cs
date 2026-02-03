using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class Incident : BaseEntity
    {
        private string _title;
        private string _description;
        private IncidentPriority _priority;
        private IncidentState _state;

        public string Title => _title;
        public string Description => _description;
        public IncidentPriority Priority => _priority;
        public IncidentState State => _state;

        public Incident()
        {
            
        }
    }
}