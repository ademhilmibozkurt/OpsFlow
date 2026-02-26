using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class IncidentTask : BaseEntity
    {
        private string _incidentId;
        private string _title;
        private string _note = "";
        private string? _abortionNote = "";
        private IncidentTaskState _taskState;
        private string _assigneeId;
        private string _assignedById;
        private string _startedById;
        private string _finishedById;
        private string _abortedById;
        private string _deletedById;

        // properties can read-only outside the class
        public string IncidentId => _incidentId;
        public string Title => _title;
        public string Note => _note;
        public string AbortionNote => _abortionNote;
        public IncidentTaskState TaskState => _taskState;
        public string AssigneeId => _assigneeId;

        private IncidentTask(string incidentId, string title, string note = "")
        {
            EnsureTitleIsValid(title);
            
            _title = title;
            _note  = note;
            _incidentId = incidentId;
            _taskState = IncidentTaskState.Created;
        }

        public static IncidentTask Create(string incidentId, string title, string note = "")
        {
            return new IncidentTask(incidentId, title, note);
        }

        private void EnsureTitleIsValid(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Task title can not be empty", nameof(title));
        }

        public void Assign(string assigneeId, string performedById)
        {   
            ChangeState(
                IncidentTaskState.Created,
                IncidentTaskState.Assigned,
                $"Task state is {_taskState}. Task can not assign!"
            );
            _assigneeId = assigneeId;
            _assignedById = performedById;
        }

        public void Start(string performedById)
        {
            ChangeState(
                IncidentTaskState.Assigned,
                IncidentTaskState.InProgress,
                $"Task state is {_taskState}. Task can not start!"
            );
            _startedById = performedById;
        }

        public void Close(string performedById)
        {
            ChangeState(
                IncidentTaskState.InProgress,
                IncidentTaskState.Done,
                $"Task state is {_taskState}. Task can not finish!"
            );
            _finishedById = performedById;
        }

        public void Abort(string abortionNote, string performedById)
        {
            if (_taskState != IncidentTaskState.Assigned && _taskState != IncidentTaskState.InProgress)
            {
                throw new InvalidOperationException($"Task state is {_taskState}. Abortion can not done!");
            }

            ChangeState(
                _taskState,
                IncidentTaskState.Aborted,
                $"Task state is {_taskState}. Abortion can not done!"
            );
            _abortedById = performedById;
            _abortionNote = abortionNote;
        }

        public void Delete(string performedById)
        {
            if (_taskState != IncidentTaskState.Done && _taskState != IncidentTaskState.Aborted)
            {
                throw new InvalidOperationException($"{_taskState} task can not delete!");
            }
            IsDeleted = true;
            _taskState = IncidentTaskState.Deleted;
            _deletedById = performedById;
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