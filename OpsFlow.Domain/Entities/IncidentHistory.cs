namespace OpsFlow.Domain.Entities
{
    public class IncidentHistory : BaseEntity
    {
        private int _incidentId;
        private Enum _eventType;
        private DateTime _occuredAt;
        private int? _relatedTaskId;
        private int _performedById;

        public IncidentHistory(int incidentId, Enum eventType, DateTime occuredAt, int? relatedTaskId, int performedById)
        {
            // every event is splitted.
            _incidentId = incidentId;
            _eventType  = eventType;
            _occuredAt  = occuredAt;
            _relatedTaskId = relatedTaskId;
            _performedById = performedById;
        }
    }
}