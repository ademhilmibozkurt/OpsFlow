using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record IncidentTimelineResponseDto
    (
        string IncidentId,
        string Title
    );
}