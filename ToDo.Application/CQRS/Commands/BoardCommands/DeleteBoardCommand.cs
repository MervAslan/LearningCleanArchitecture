using AutoMapper;
using MediatR;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;

namespace ToDo.Application.CQRS.Commands.BoardCommands
{
    public class DeleteBoardCommand : IRequest<Result<bool>>
    {
        public int CategoryId { get; set; } 
        public int BoardId { get; set; }
    }
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Result<bool>>
    {
        IBoardRepository _boardRepository;
        ICurrentUserService _currentUserService;
        IMapper _mapper;
       
        public DeleteBoardCommandHandler(IBoardRepository boardRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            
        }
        public async Task<Result<bool>> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if(currentUserId == null) return Result<bool>.Failure("Oturum bulunamadı.");

           var board = await _boardRepository.GetByIdAsync(request.BoardId);
           _boardRepository.Delete(board);
           await _boardRepository.SaveChangesAsync();
           return Result<bool>.Success(true, "Board başarıyla silindi.");





        }
    }
}
