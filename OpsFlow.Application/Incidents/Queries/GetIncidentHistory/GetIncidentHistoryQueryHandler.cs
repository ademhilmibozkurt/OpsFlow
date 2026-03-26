using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentHistory
{
    public class GetIncidentHistoryQueryHandler : IRequestHandler<GetIncidentHistoryQuery, PaginatedResponseDto<HistoryItemDto>>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;

        public GetIncidentHistoryQueryHandler
        (
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService
        )
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }
        
        public async Task<PaginatedResponseDto<HistoryItemDto>> Handle(GetIncidentHistoryQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // findIncident
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");

            // checkPermission
            _permissionService.CanGetIncidentHistory(incident.CreatedById, userId, userRole);

             // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

             // getQuery
            IQueryable<IncidentHistory> query = _historyRepository.Query(cancellationToken);

            // getHistorys
            query = query.Where(x => x.IncidentId == request.incidentId);

            // orderByDate
            query = query.OrderByDescending(x => x.OccuredAt);

            // getTotalCount
            int totalCount = query.Count();

            // paginate
            var items = query
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .Select(x => new HistoryItemDto
                (
                    incident.Id,
                    x.PerformedById,
                    x.EventType,
                    x.OccuredAt,
                    x.Note
                )).ToList();

            //  returnDto
            return new PaginatedResponseDto<HistoryItemDto>
            (
                items,
                pageNumber,
                pageSize,
                totalCount
            );
        }
    }
}