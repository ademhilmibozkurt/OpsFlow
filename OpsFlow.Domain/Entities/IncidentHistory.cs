using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class IncidentHistory : BaseEntity
    {
        private string _incidentId;
        private Enum _eventType;
        private DateTime _occuredAt;
        private string _performedById;
        private string _taskId;
        private string _note;

        private IncidentHistory(string incidentId, string performedById, IncidentState eventType, DateTime occuredAt, string note)
        {
            _incidentId = incidentId;
            _performedById = performedById;
            _eventType  = eventType;
            _occuredAt  = occuredAt;
            _note = note;
        }

        private IncidentHistory(string incidentId, string performedById, IncidentTaskState eventType, DateTime occuredAt, string taskId, string note)
        {
            _incidentId = incidentId;
            _performedById = performedById;
            _eventType  = eventType;
            _occuredAt  = occuredAt;
            _note = note;

            EnsureRelatedTaskId(taskId);
            _taskId = taskId;
        }

        private IncidentHistory(string incidentId, string performedById, IncidentPriority eventType, DateTime occuredAt, string note)
        {
            _incidentId = incidentId;
            _performedById = performedById;
            _eventType  = eventType;
            _occuredAt  = occuredAt;
            _note = note;
        }

        // add incident history with factory
        public static IncidentHistory AddIncidentHistory(string incidentId, string performedById, IncidentState eventType, DateTime occuredAt, string note = "")
        {
            return new IncidentHistory(incidentId, performedById, eventType, occuredAt, note);
        }

        // add task history with factory
        public static IncidentHistory AddTaskHistory(string incidentId, string performedById, IncidentTaskState eventType, DateTime occuredAt, string taskId, string note = "")
        {

            return new IncidentHistory(incidentId, performedById, eventType, occuredAt, taskId, note);
        }

        public static IncidentHistory AddPriorityHistory(string incidentId, string performedById, IncidentPriority eventType, DateTime occuredAt, string note = "")
        {
            return new IncidentHistory(incidentId, performedById, eventType, occuredAt, note);
        }

        private void EnsureRelatedTaskId(string? id)
        {
            if (id == null)
            {
                return;
            }
        }
    }
}