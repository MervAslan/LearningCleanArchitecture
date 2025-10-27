using AutoMapper;
using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;


namespace ToDo.Application.CQRS.Commands.TaskCommands
{
    public class UpdateTaskStatusCommand : IRequest<Result<TaskItemDto>>
    {
        public int TaskItemId { get; set; }
        public string newStatus { get; set; }
        public int CategoryId { get; set; }
        public int BoarId { get; set; }
    }
    public class UpdateTaskStatusCommandHandler: IRequestHandler<UpdateTaskStatusCommand, Result<TaskItemDto>>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public UpdateTaskStatusCommandHandler(ITaskItemRepository taskItemRepository, ICurrentUserService currentUserService,IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<Result<TaskItemDto>> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if (currentUserId == null) return Result<TaskItemDto>.Failure("Oturum bulunamadı.");
            var taskItem = await _taskItemRepository.GetByIdAsync(request.TaskItemId);
            if (taskItem == null) return Result<TaskItemDto>.Failure("Task bulunamadı veya erişim izniniz yok.");

            taskItem.Status = request.newStatus;

            _taskItemRepository.Update(taskItem);
            await _taskItemRepository.SaveChangesAsync();
            var taskItemDto = _mapper.Map<TaskItemDto>(taskItem);
            return Result<TaskItemDto>.Success(taskItemDto, "Statü başarıyla güncellendi.");
        }
    }
}
