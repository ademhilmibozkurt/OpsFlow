using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Tasks.Commands.StartTask;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Commands.AssignTask
{
    public class StartTaskCommandHandler : IRequestHandler<StartTaskCommand, StartTaskResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public StartTaskCommandHandler(
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

        public async Task<StartTaskResponseDto> Handle(StartTaskCommand request, CancellationToken cancellationToken)
        {
             // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User id not found!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User role not found!");

            // findTask
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");

            IncidentTask task = incident.GetTask(request.taskId);
            
            // checkPermission
            _permissionService.CanStartTask(userId, task.AssigneeId);

            // startTask
            task.Start(userId);
            DateTime startedAt = _timeProvider.Now();

            // addHistory
            IncidentHistory history = IncidentHistory.AddTaskHistory(incident.Id, userId, IncidentTaskState.InProgress, startedAt, task.Id);     
            await _historyRepository.AddAsync(history, cancellationToken);

            // save
            _unitOfWork.CommitAsync(cancellationToken);

            return new StartTaskResponseDto
            (
                task.Id,
                task.Title,
                userId,
                startedAt
            );
        }
    }
}