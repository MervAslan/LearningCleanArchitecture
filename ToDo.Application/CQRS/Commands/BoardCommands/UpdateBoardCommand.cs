using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;
using AutoMapper;

namespace ToDo.Application.CQRS.Commands.BoardCommands
{
    public class UpdateBoardCommand : IRequest<Result<BoardDto>>
    {
        public string newTitle { get; set; }
        public int BoardId { get; set; }
    }
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, Result<BoardDto>>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public UpdateBoardCommandHandler(IBoardRepository boardRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<Result<BoardDto>> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if (currentUserId == null) return Result<BoardDto>.Failure("Oturum bulunamadı.");

            var board = await _boardRepository.GetByIdAsync(request.BoardId);
            
            if (string.IsNullOrWhiteSpace(request.newTitle)) return Result<BoardDto>.Failure("Yeni başlık boş olamaz.");
            var existingBoard = await _boardRepository.GetByNameAsync(request.newTitle);
            if(existingBoard != null && existingBoard.Id != request.BoardId) return Result<BoardDto>.Failure("Bu isimde başka bir board zaten mevcut.");

            board.Title = request.newTitle;
            _boardRepository.Update(board);
            await _boardRepository.SaveChangesAsync();
            var boardDto = _mapper.Map<BoardDto>(board);
            return Result<BoardDto>.Success(boardDto, "Board başarıyla güncellendi.");
        }
    }
}
