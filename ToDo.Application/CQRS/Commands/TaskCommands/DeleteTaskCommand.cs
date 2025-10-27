using MediatR;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;

namespace ToDo.Application.CQRS.Commands.TaskCommands
{
    public class DeleteTaskCommand : IRequest<Result<bool>>
    {
        public int TaskItemId { get; set; }
        public int BoardId { get; set; }
        public int CategoryId { get; set; }
    }
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result<bool>>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteTaskCommandHandler(ITaskItemRepository taskItemRepository, ICurrentUserService currentUserService)
        {
            _taskItemRepository = taskItemRepository;
            _currentUserService = currentUserService;
        }
        public async Task<Result<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if (currentUserId == null) return Result<bool>.Failure("Oturum bulunamadı.");
            var taskItem = await _taskItemRepository.GetByIdAsync(request.TaskItemId);
            if (taskItem == null || taskItem.BoardId != request.BoardId) return Result<bool>.Failure("Task bulunamadı veya erişim izniniz yok.");
            _taskItemRepository.Delete(taskItem);
            await _taskItemRepository.SaveChangesAsync();
            return Result<bool>.Success(true, "Task başarıyla silindi.");
        }
    }
}