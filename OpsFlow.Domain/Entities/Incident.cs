using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class Incident : BaseEntity
    {
        private string _title;
        private string _description;
        private string? _abortionNote;
        private string _createdById;
        private string? _invastigateById;
        private string? _closedById;
        private string? _abortedById;
        private string? _deletedById;
        private string? _settedById;
        private IncidentPriority _priority;
        private IncidentState _state;
        private List<IncidentTask>? _tasks = new List<IncidentTask>();

        public string Title => _title;
        public string Description => _description;
        public string? AbortionNote => _abortionNote;
        public string CreatedById => _createdById;
        public IncidentPriority Priority => _priority;
        public IncidentState State => _state;
        public List<IncidentTask>? Tasks => _tasks;

        
        private Incident(string title, string description, string createdById)
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
        public static Incident Create(string title, string description, string createdById)
        {
            return new Incident(title, description, createdById);
        }

        private void EnsureIsValid(string text, string name)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException($"-{name}- can not be empty!");
        }

        public void Close(string performedById)
        {
            if (_state == IncidentState.Aborted || _state == IncidentState.Closed)
            {
                throw new InvalidOperationException($"{_state} incident can not close!");
            }
            else if (_state == IncidentState.Open)
            {
                throw new InvalidOperationException("Incident not investigated yet. Can not close!");
            }
            
            EnsureTasksDone();
            _state = IncidentState.Closed;
            _closedById = performedById;
        }

        public void Investigate(string performedById)
        {
            if(_state != IncidentState.Open)
            {
                throw new InvalidOperationException("Incident is not open!");
            }
            _state = IncidentState.Investigating;
            _invastigateById = performedById;
        }

        public void Abort(string abortionNote, string performedById)
        {
            if (_state == IncidentState.Aborted || _state == IncidentState.Closed)
            {
                throw new InvalidOperationException($"{_state} incident can not abort!");
            }
            _state = IncidentState.Aborted;
            _abortedById = performedById;
            _abortionNote = abortionNote;
        }

        public void Delete(string performedById)
        {
            if (_state != IncidentState.Aborted || _state != IncidentState.Closed)
            {
                throw new InvalidOperationException($"{_state} incident can not delete!");
            }
            IsDeleted = true;
            _state = IncidentState.Deleted;
            _deletedById = performedById;
        }

        public void SetPriority(IncidentPriority toPriority, string performedById)
        {
            if (_priority == toPriority)
            {
                throw new InvalidOperationException($"Priority is already {_priority}. Can not change!");
            }
            _priority = toPriority;
            _settedById = performedById;
        } 

        public void AddTask(IncidentTask task)
        {
            if (_state != IncidentState.Open)
            {
                throw new InvalidOperationException("Incident is not open. Can not add task!");
            }

            _tasks?.Append(task);
        }

        public IncidentTask GetTask(string taskId)
        {
            IncidentTask task = _tasks?.Find
            (
                t => t.Id == taskId
            ) ?? throw new NullReferenceException("Task not found. Task does not exist!");
            return task;
        }

        public void DropTask(string taskId)
        {
            IncidentTask task = _tasks?.Find
            (
                t => t.Id == taskId
            ) ?? throw new NullReferenceException("Task not found. Task does not exist!");
            _tasks.Remove(task);
        }

        public void EnsureTasksDone()
        {
            if(_tasks == null) throw new Exception();
            foreach(IncidentTask task in _tasks)
            {
                if (task.TaskState != IncidentTaskState.Done)
                {
                    throw new InvalidOperationException("All tasks are not done. Can not close the incident!");
                }
            }
        }

        public bool IsAllTasksDone()
        {
            if(_tasks == null) throw new Exception();
            foreach(IncidentTask task in _tasks)
            {
                if (task.TaskState != IncidentTaskState.Done)
                {
                    return false;
                }
            }
            return true;
        }

        public int TaskCount()
        {

            return _tasks == null ? throw new Exception() : _tasks.Count;
        }
        
        public int OpenTaskCount()
        {
            int count = 0;
            if(_tasks == null) throw new Exception();
            foreach(IncidentTask task in _tasks)
            {
                if (task.TaskState == IncidentTaskState.Assigned ||
                    task.TaskState == IncidentTaskState.Created ||
                    task.TaskState == IncidentTaskState.InProgress)
                {
                    count += 1;
                }
            }
            return count;
        }

        public int CompletedTaskCount()
        {
            int count = 0;
            if(_tasks == null) throw new Exception();
            foreach(IncidentTask task in _tasks)
            {
                if (task.TaskState == IncidentTaskState.Done ||
                    task.TaskState == IncidentTaskState.Aborted ||
                    task.TaskState == IncidentTaskState.Deleted)
                {
                    count += 1;
                }
            }
            return count;
        }
    }
}