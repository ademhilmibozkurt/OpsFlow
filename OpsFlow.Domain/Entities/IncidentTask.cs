using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class IncidentTask : BaseEntity
    {
        // incident has not change after entity created
        private readonly Incident _incident;

        private string _title = "";
        private string _note = "";
        private IncidentTaskState _taskState;

        // properties can read only outside the class
        public string Title => _title;
        public string Note => _note;
        public IncidentTaskState TaskState => _taskState;

        public IncidentTask(Incident incident, string title, string note = "")
        {
            // ensure incident exists. readonly property only set in ctor.
            _incident = incident ?? throw new ArgumentNullException(nameof(incident));

            EnsureIncidentIsOpen();
            EnsureTitleIsValid(title);

            _title = title;
            _note  = note;
            _taskState = IncidentTaskState.Appointed;
        }

        private void EnsureIncidentIsOpen()
        {
            if (_incident.State != IncidentState.Open)
            {
                throw new InvalidOperationException("Incident is not open!");
            }
        }

        private void EnsureTitleIsValid(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Task title can not be empty", nameof(title));
        }

        public void Start()
        {
            ChangeState(
                IncidentTaskState.Appointed,
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
            if (_taskState != IncidentTaskState.Appointed && _taskState != IncidentTaskState.InProgress)
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
            EnsureIncidentIsOpen();

            if (_taskState != fromState)
            {
                throw new InvalidOperationException(errorMessage);
            }
            
            _taskState = toState;
        }
    }
}