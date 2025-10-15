using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;
using AutoMapper;

namespace ToDo.Application.CQRS.Commands.CategoryCommands
{
    public class UpdateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public int CategoryId { get; set; }
        public string NewCategoryName { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if (currentUserId == null) return Result<CategoryDto>.Failure("Oturum bulunamadı.");

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null || category.UserId != currentUserId) return Result<CategoryDto>.Failure("Kategori bulunamadı veya erişim izniniz yok.");

            if (string.IsNullOrWhiteSpace(request.NewCategoryName)) return Result<CategoryDto>.Failure("Yeni kategori adı boş olamaz.");

            var existingCategory = await _categoryRepository.GetByNameAndUserIdAsync(request.NewCategoryName, currentUserId);
            if (existingCategory != null && existingCategory.Id != request.CategoryId) return Result<CategoryDto>.Failure("Bu isimde başka bir kategori zaten mevcut.");


            category.Name = request.NewCategoryName; 
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Success(categoryDto, "Kategori başarıyla güncellendi.");
        }
    }
}
