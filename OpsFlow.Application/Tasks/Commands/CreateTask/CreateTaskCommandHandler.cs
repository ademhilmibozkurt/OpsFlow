using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        public CreateTaskCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateTaskResponseDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new NotFoundException("User id not found!");
            string userRole = _currentUser.Role ?? throw new NotFoundException("User role not found!");

            // checkPermission
            _permissionService.CanCreateTask(userRole);

            // checkIncident
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");

            EnsureIncidentOpen(incident);

            // createTask
            IncidentTask task = IncidentTask.Create(request.incidentId, request.title, request.note);
            
            incident.AddTask(task);
            DateTime createdAt = _timeProvider.Now();
            task.CreatedAt = createdAt;

            // addHistory 
            IncidentHistory history = IncidentHistory.AddTaskHistory(incident.Id, userId, IncidentTaskState.Created, createdAt, task.Id);
            await _historyRepository.AddAsync(history, cancellationToken);

            // save
            _unitOfWork.CommitAsync(cancellationToken);

            return new CreateTaskResponseDto
            (
                task.Id,
                incident.Id,
                request.title,
                request.note,
                userId,
                createdAt
            );
        }

        private void EnsureIncidentOpen(Incident incident)
        {
            if (incident.State != IncidentState.Open)
            {
                throw new InvalidOperationException("Incident is not open. Can not add task!");
            }
        }
    }
}