namespace OpsFlow.Domain.Entities
{
    public class IncidentHistory : BaseEntity
    {
        private int _incidentId;
        private Enum _eventType;
        private DateTime _occuredAt;
        private int _performedById;
        private int? _relatedTaskId;

        public IncidentHistory(int incidentId, Enum eventType, DateTime occuredAt, int performedById, int? relatedTaskId)
        {
            // every event is splitted.
            EnsureIdPositive(incidentId, "incidentId");
            EnsureIdPositive(performedById, "performedById");

            _incidentId = incidentId;
            _eventType  = eventType;
            _occuredAt  = occuredAt;
            _performedById = performedById;

            EnsureRelatedTaskId(relatedTaskId);
            _relatedTaskId = relatedTaskId;
        }

        private void EnsureIdPositive(int id, string name)
        {
            if (id < 0)
            {
                throw new ArgumentException($"-{name}- can not be negative numbers!");
            }
        }

        private void EnsureRelatedTaskId(int? id)
        {
            if (id == null)
            {
                return;
            }
            if (id < 0)
            {
                throw new ArgumentException("-relatedTaskId- can not be negative numbers!");
            }
        }
    }
}