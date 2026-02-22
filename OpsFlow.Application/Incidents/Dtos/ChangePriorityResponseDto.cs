namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record ChangePriorityResponseDto
    (
        string IncidentId,
        string Priority,
        string ChangedById,
        DateTime OccuredAt
    );
}