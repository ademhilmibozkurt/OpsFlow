using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.ChangePriority
{
    public record ChangePriorityCommand(int incidentId, IncidentPriority toPriority);
}