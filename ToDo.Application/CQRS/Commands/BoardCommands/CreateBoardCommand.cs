using AutoMapper;
using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.CQRS.Commands.BoardCommands
{
    public  class CreateBoardCommand : IRequest<Result<BoardDto>>
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Result<BoardDto>>
    {
        IBoardRepository _boardRepository;
        ICurrentUserService _currentUserService;
        IMapper _mapper;
        ICategoryRepository _categoryRepository;
        public CreateBoardCommandHandler(IBoardRepository boardRepository, ICurrentUserService currentUserService, IMapper mapper,ICategoryRepository categoryRepository)
        {
            _boardRepository = boardRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<BoardDto>> Handle(CreateBoardCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Title)) return Result<BoardDto>.Failure("Board başlığı boş olamaz.");

            var category = await _categoryRepository.GetByIdAsync(command.CategoryId);

            if (category == null || category.UserId != _currentUserService.CurrentUserId) return Result<BoardDto>.Failure("Kategori bulunamadı veya erişim izniniz yok.");

            var board =_mapper.Map<Board>(command);
            board.CategoryId = command.CategoryId;
            await _boardRepository.AddAsync(board);
            await _boardRepository.SaveChangesAsync();
            var boardDto = _mapper.Map<BoardDto>(board);
            return Result<BoardDto>.Success(boardDto, "Board başarıyla oluşturuldu.");




        }

    }

}
