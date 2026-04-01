using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Incidents.Dtos;

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

            // getQueries - join + permission check
            var query =
                from h in _historyRepository.Query(cancellationToken)
                join i in _incidentRepository.Query(cancellationToken)
                    on h.IncidentId equals i.Id
                where i.Id == request.incidentId
                    && 
                    (
                            _permissionService.CanGetIncidentHistory(i.CreatedById, userId, userRole)
                    )
                select new HistoryItemDto
                (
                    i.Id,
                    h.PerformedById,
                    h.EventType,
                    h.OccuredAt,
                    h.Note
                );

            // getTotalCount
            int totalCount = query.Count();

            // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // paginate + sort
            var items = query
                .OrderByDescending(x => x.OccuredAt)
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToList()
                ?? throw new NotFoundException("Query result not found!");

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