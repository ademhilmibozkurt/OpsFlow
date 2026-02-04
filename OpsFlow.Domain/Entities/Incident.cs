using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class Incident : BaseEntity
    {
        private string _title;
        private string _description;
        private int _createdById;
        private int _invastigateById;
        private int _abortedById;
        private IncidentPriority _priority;
        private IncidentState _state;
        private List<IncidentTask> _tasks;

        public string Title => _title;
        public string Description => _description;
        public IncidentPriority Priority => _priority;
        public IncidentState State => _state;

        // Authentication -> Application Layer
        // Admin, Incident manager and user that created the incident allow to close incident -> Application Layer
        public Incident(string title, string description, int createdById, List<IncidentTask> tasks)
        {
            // tasklar hallolduysa incident kapanır!!!
            EnsureIsValid(title, "title");
            EnsureIsValid(description, "description");

            _title = title;
            _description = description;
            _createdById = createdById;
            _tasks = tasks;
            _priority = IncidentPriority.Normal;
            _state = IncidentState.Open;
        }

        private void EnsureIsValid(string text, string name)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException($"-{name}- can not be empty!");
        }

        public void Close()
        {
            EnsureTasksDone();
            if (_state == IncidentState.Aborted || _state == IncidentState.Closed)
            {
                throw new InvalidOperationException($"{_state} incident can not close!");
            }
            else if (_state == IncidentState.Open)
            {
                throw new InvalidOperationException("Incident not investigated yet. Can not close!");
            }
            _state = IncidentState.Closed;
        }

        private void EnsureTasksDone()
        {
            foreach(IncidentTask t in _tasks)
            {
                if(t.TaskState == IncidentTaskState.InProgress || t.TaskState == IncidentTaskState.Appointed)
                {
                    throw new InvalidOperationException("All tasks have to finish for closing the incident!");
                }   
            }
        }

        public void Investigate()
        {
            if(_state != IncidentState.Open)
            {
                throw new InvalidOperationException("Incident is not open!");
            }
            _state = IncidentState.Investigating;
        }

        public void Abort()
        {
            if (_state == IncidentState.Aborted || _state == IncidentState.Closed)
            {
                throw new InvalidOperationException($"{_state} incident can not abort!");
            }
            _state = IncidentState.Aborted;
        }

        public void SetPriority(IncidentPriority toPriority)
        {
            if (_priority == toPriority)
            {
                throw new InvalidOperationException($"Priority is already {_priority}. Can not change!");
            }
            _priority = toPriority;
        } 
    }
}