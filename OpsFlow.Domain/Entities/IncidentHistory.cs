using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class IncidentHistory : BaseEntity
    {
        private int _incidentId;
        private Enum _eventType;
        private DateTime _occuredAt;
        private int _performedById;
        private int? _taskId;

        private IncidentHistory(int incidentId, int performedById, IncidentState eventType, DateTime occuredAt)
        {
            // every event is splitted.
            EnsureIdPositive(incidentId, "incidentId");
            EnsureIdPositive(performedById, "performedById");

            _incidentId = incidentId;
            _performedById = performedById;
            _eventType  = eventType;
            _occuredAt  = occuredAt;
        }

        private IncidentHistory(int incidentId, int performedById, IncidentTaskState eventType, DateTime occuredAt, int taskId)
        {
            // every event is splitted.
            EnsureIdPositive(incidentId, "incidentId");
            EnsureIdPositive(performedById, "performedById");

            _incidentId = incidentId;
            _performedById = performedById;
            _eventType  = eventType;
            _occuredAt  = occuredAt;

            EnsureRelatedTaskId(taskId);
            _taskId = taskId;
        }

        // add incident history with factory
        public static IncidentHistory AddIncidentHistory(int incidentId, int performedById, IncidentState eventType, DateTime occuredAt)
        {
            return new IncidentHistory(incidentId, performedById, eventType, occuredAt);
        }

        // add task history with factory
        public static IncidentHistory AddTaskHistory(int incidentId, int performedById, IncidentTaskState eventType, DateTime occuredAt, int taskId)
        {
            return new IncidentHistory(incidentId, performedById, eventType, occuredAt, taskId);
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