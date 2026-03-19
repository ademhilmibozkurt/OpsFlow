using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Application.Incidents.Queries.GetIncidents;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentHistory
{
    public class GetIncidentHistoryQueryHandler : IRequestHandler<GetIncidentsQuery, HistoryItemDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;

        public GetIncidentHistoryQueryHandler
        (
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUser
        )
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUser = currentUser;
        }
        
        public async Task<HistoryItemDto> Handle(GetIncidentsQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser

            // findIncident

            // checkPermission

            // getHistorys

            // createHistoryItem

            // orderByDate

            //  returnDto
        }
    }
}