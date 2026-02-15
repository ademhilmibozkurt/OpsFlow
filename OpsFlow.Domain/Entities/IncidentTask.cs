using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class IncidentTask : BaseEntity
    {
        // incident has not change after entity created
        private readonly Incident _incident;
        private int _incidentId;

        private string _title = "";
        private string _note = "";
        private IncidentTaskState _taskState;
        private int _assignedId;

        // properties can read-only outside the class
        public int IncidentId => _incidentId;
        public string Title => _title;
        public string Note => _note;
        public IncidentTaskState TaskState => _taskState;
        public int AssignedId => _assignedId;

        private IncidentTask(int incidentId, string title, string note = "")
        {
            EnsureTitleIsValid(title);
            
            _title = title;
            _note  = note;
            _incidentId = incidentId;
            _taskState = IncidentTaskState.Created;
        }

        public static IncidentTask Create(int incidentId, string title, string note = "")
        {
            return new IncidentTask(incidentId, title, note);
        }

        private void EnsureTitleIsValid(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Task title can not be empty", nameof(title));
        }

        public void Assign(int assignedId)
        {   
            ChangeState(
                IncidentTaskState.Created,
                IncidentTaskState.Assigned,
                $"Task state is {_taskState}. Task can not assign!"
            );
            _assignedId = assignedId;
        }

        public void Start()
        {
            ChangeState(
                IncidentTaskState.Assigned,
                IncidentTaskState.InProgress,
                $"Task state is {_taskState}. Task can not start!");
        }

        public void Finish()
        {
            ChangeState(
                IncidentTaskState.InProgress,
                IncidentTaskState.Done,
                $"Task state is {_taskState}. Task can not finish!");
        }

        public void Abort()
        {
            if (_taskState != IncidentTaskState.Assigned && _taskState != IncidentTaskState.InProgress)
            {
                throw new InvalidOperationException($"Task state is {_taskState}. Abortion can not done!");
            }

            ChangeState(
                _taskState,
                IncidentTaskState.Aborted,
                $"Task state is {_taskState}. Abortion can not done!");
        }

        private void ChangeState(IncidentTaskState fromState, IncidentTaskState toState, string errorMessage)
        {
            if (_taskState != fromState)
            {
                throw new InvalidOperationException(errorMessage);
            }
            
            _taskState = toState;
        }
    }
}