using AutoMapper;
using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces;
using ToDo.Application.Helpers;
using ToDo.Domain.Entities;

namespace ToDo.Application.CQRS.Commands.CategoryCommands
{
    public class CreateCategoryCommand : IRequest<Result<CategoryDto>>
    {
       public string Name { get; set; }

       

    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if (currentUserId == null) return Result<CategoryDto>.Failure("Oturum bulunamadı.");

            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<CategoryDto>.Failure("Kategori adı boş olamaz.");

            var existingCategory = await _categoryRepository.GetByNameAndUserIdAsync(request.Name, currentUserId);
            if (existingCategory != null) return Result<CategoryDto>.Failure("Kategori zaten mevcut.");

            var category = _mapper.Map<Category>(request);
            category.UserId = currentUserId;

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Success(categoryDto, "Kategori başarıyla oluşturuldu.");


        }
    }
}
