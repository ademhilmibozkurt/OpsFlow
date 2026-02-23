using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.DeleteIncident
{
    public class DeleteIncidentCommandHandler : IRequestHandler<DeleteIncidentCommand, DeleteIncidentResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteIncidentCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUserService,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUserService = currentUserService;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public Task<DeleteIncidentResponseDto> Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
        {
            User user = _currentUserService.Get();

            _permissionService.CanDeleteIncident(user);

            Incident incident = await _incidentRepository.GetByIdAsync(command.incidentId);
            incident.Delete(user.Id);

            IncidentHistory history = IncidentHistory.AddIncidentHistory(incident.Id, user.Id, IncidentState.Deleted, _timeProvider.Now());
            await _historyRepository.AddAsync(history);

            _unitOfWork.Commit();

            return incident.Id;
        }
    }
}