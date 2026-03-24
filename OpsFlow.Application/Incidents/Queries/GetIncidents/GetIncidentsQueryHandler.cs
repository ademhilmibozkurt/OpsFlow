using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Incidents.Queries.GetIncidents
{
    public class GetIncidentsQueryHandler : IRequestHandler<GetIncidentsQuery, PaginatedResponseDto<IncidentListItemDto>>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUser;

        public GetIncidentsQueryHandler
        (
            IIncidentRepository incidentRepository,
            ICurrentUserService currentUser
        )
        {
            _incidentRepository = incidentRepository;
            _currentUser = currentUser;
        }

        public async Task<PaginatedResponseDto<IncidentListItemDto>> Handle(GetIncidentsQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // getQuery
            IQueryable<Incident> query = _incidentRepository.Query(cancellationToken);

            // roleBaseFiltering
            if (userRole != "Admin")
            {
                query = query.Where(x => x.CreatedById == userId);
            }

            // addOptionalFiltering
                // filterByState
            if (request.State != null)
            {
                query = query.Where(x => x.State == request.State);
            }

                // filterByPriority
            if (request.Priority != null)
            {
                query = query.Where(x => x.Priority == request.Priority);
            }

            // getTotalCount
            int totalCount = query.Count();

            // sortByCreationDate
            query = query.OrderByDescending(x => x.CreatedAt);

            // paginate
            var items = query
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .Select(x => new IncidentListItemDto
                (
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Priority,
                    x.State,
                    x.CreatedAt
                )).ToList();

            // returnDto
            return new PaginatedResponseDto<IncidentListItemDto>
            (
                items,
                pageNumber,
                pageSize,
                totalCount
            );
        }
    }
}