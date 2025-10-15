using MediatR;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;

namespace ToDo.Application.CQRS.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : IRequest<Result<bool>>
    {
        public int CategoryId { get; set; }
        
    }
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }
        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if (currentUserId == null) return Result<bool>.Failure("Oturum bulunamadı.");

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null || category.UserId != currentUserId) return Result<bool>.Failure("Kategori bulunamadı veya erişim izniniz yok.");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
            return Result<bool>.Success(true, "Kategori başarıyla silindi.");
        }
    }



}
