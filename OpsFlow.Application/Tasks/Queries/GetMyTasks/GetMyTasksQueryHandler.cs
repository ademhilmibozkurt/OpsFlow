using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Tasks.Queries.GetMyTasks
{
    public class GetMyTasksQueryHandler : IRequestHandler<GetMyTasksQuery, PaginatedResponseDto<TaskListItemDto>>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUser;
        
        public GetMyTasksQueryHandler
        (
            IIncidentRepository incidentRepository,
            ICurrentUserService currentUser    
        )
        {
            _incidentRepository = incidentRepository;
            _currentUser = currentUser;
        }

        public async Task<PaginatedResponseDto<TaskListItemDto>> Handle(GetMyTasksQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");

            // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // getIncidentQuery
            IQueryable<IncidentTask> query = _incidentRepository.TaskQuery(cancellationToken);

            // getMyTasks
            query = query.Where
            (
                x => x.AssigneeId == userId
            ) 
            ?? throw new NotFoundException("Task not found!");

            // sorting
            query = query.OrderByDescending(x => x.CreatedAt);

            // getTotalCount
            int totalCount = query.Count();

            // pagination
            var items = query
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .Select(x => new TaskListItemDto
                (
                    x.Id,
                    x.IncidentId,
                    x.CreatedById,
                    x.AssigneeId,
                    x.Title,
                    x.Note,
                    x.TaskState,
                    x.CreatedAt,
                    x.AbortionNote
                )).ToList();

            // returnDto
            return new PaginatedResponseDto<TaskListItemDto>
            (
                items,
                pageNumber,
                pageSize,
                totalCount
            );
        }
    }
}