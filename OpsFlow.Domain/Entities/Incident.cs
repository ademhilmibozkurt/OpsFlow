using System.Data;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class Incident : BaseEntity
    {
        private string _title;
        private string _description;
        private int _createdById;
        private int _invastigateById;
        private int _closedById;
        private int _abortedById;
        private int _settedById;
        private IncidentPriority _priority;
        private IncidentState _state;

        public string Title => _title;
        public string Description => _description;
        public int CreatedById => _createdById;
        public IncidentPriority Priority => _priority;
        public IncidentState State => _state;

        
        private Incident(string title, string description, int createdById)
        {
            EnsureIsValid(title, "title");
            EnsureIsValid(description, "description");

            _title = title;
            _description = description;
            _createdById = createdById;
            _priority = IncidentPriority.Normal;
            _state = IncidentState.Open;
        }

        // create with factory method
        public static Incident Create(string title, string description, int createdById)
        {
            return new Incident(title, description, createdById);
        }

        private void EnsureIsValid(string text, string name)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException($"-{name}- can not be empty!");
        }

        public void Close(bool isTasksDone, int performedById)
        {
            if (_state == IncidentState.Aborted || _state == IncidentState.Closed)
            {
                throw new InvalidOperationException($"{_state} incident can not close!");
            }
            else if (_state == IncidentState.Open)
            {
                throw new InvalidOperationException("Incident not investigated yet. Can not close!");
            }
            if(!isTasksDone)
            {
                throw new InvalidOperationException("Incident can not close because all tasks are not done yet!");
            }
            _state = IncidentState.Closed;
            _closedById = performedById;
        }

        public void Investigate(int performedById)
        {
            if(_state != IncidentState.Open)
            {
                throw new InvalidOperationException("Incident is not open!");
            }
            _state = IncidentState.Investigating;
            _invastigateById = performedById;
        }

        public void Abort(int performedById)
        {
            if (_state == IncidentState.Aborted || _state == IncidentState.Closed)
            {
                throw new InvalidOperationException($"{_state} incident can not abort!");
            }
            _state = IncidentState.Aborted;
            _abortedById = performedById;
        }

        public void SetPriority(IncidentPriority toPriority, int performedById)
        {
            if (_priority == toPriority)
            {
                throw new InvalidOperationException($"Priority is already {_priority}. Can not change!");
            }
            _priority = toPriority;
            _settedById = performedById;
        } 
    }
}