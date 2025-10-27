using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;
using AutoMapper;

namespace ToDo.Application.CQRS.Commands.TaskCommands
{
    public class UpdateTaskCommand : IRequest<Result<TaskItemDto>>
    {
        public int TaskItemId { get; set; }
        public string Title { get; set; }
        public string ?Description { get; set; }
        public string? Tag { get; set; }
        public int BoardId { get; set; }
        public int CategoryId { get; set; }
    }
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result<TaskItemDto>>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public UpdateTaskCommandHandler(ITaskItemRepository taskItemRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<Result<TaskItemDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if (currentUserId == null) return Result<TaskItemDto>.Failure("Oturum bulunamadı.");
            var taskItem = await _taskItemRepository.GetByIdAsync(request.TaskItemId);
            if (taskItem == null) return Result<TaskItemDto>.Failure("Task bulunamadı veya erişim izniniz yok.");
            if (string.IsNullOrEmpty(request.Title)) return Result<TaskItemDto>.Failure("Task başlığı boş olamaz.");
            taskItem.Title = request.Title;
            taskItem.Description = request.Description;
            taskItem.Tag = request.Tag;

            _taskItemRepository.Update(taskItem);
            await _taskItemRepository.SaveChangesAsync();
            var taskItemDto = _mapper.Map<TaskItemDto>(taskItem);
            return Result<TaskItemDto>.Success(taskItemDto, "Task başarıyla güncellendi.");
        }
    }
}
