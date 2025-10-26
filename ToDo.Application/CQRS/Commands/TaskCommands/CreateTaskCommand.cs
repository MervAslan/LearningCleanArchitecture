using MediatR;
using ToDo.Application.Helpers;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces;
using AutoMapper;

namespace ToDo.Application.CQRS.Commands.TaskCommands
{
    public class CreateTaskCommand : IRequest<Result<TaskItemDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Tag { get; set; }
        public int BoardId { get; set; }
    }
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<TaskItemDto>>
    {
        ITaskItemRepository _taskItemRepository;
        IBoardRepository _boardRepository;
        ICurrentUserService _currentUserService;
        IMapper _mapper;
        public CreateTaskCommandHandler(ITaskItemRepository taskItemRepository, IBoardRepository boardRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _boardRepository = boardRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<Result<TaskItemDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.Title)) return Result<TaskItemDto>.Failure("Task başlığı boş olamaz.");
            var board = await _boardRepository.GetByIdAsync(request.BoardId);
            
            var taskItem = _mapper.Map<Domain.Entities.TaskItem>(request);
            taskItem.Status = "ToDo";
            taskItem.DueDate = DateTime.UtcNow;
            taskItem.BoardId = request.BoardId;
            await _taskItemRepository.AddAsync(taskItem);
            await _taskItemRepository.SaveChangesAsync();
            var taskItemDto = _mapper.Map<TaskItemDto>(taskItem);
            return Result<TaskItemDto>.Success(taskItemDto, "Task başarıyla oluşturuldu.");

        }
    }
}
