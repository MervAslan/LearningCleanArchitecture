using MediatR;
using System.ComponentModel.DataAnnotations;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;
using AutoMapper;

namespace ToDo.Application.CQRS.Queries.UserQueries
{
    public class LoginUserQuery : IRequest<Result<UserDto>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null) return Result<UserDto>.Failure("Kullanıcı bulunamadı.");

            var hashedPassword = HashPassword(request.Password);
            if (user.PasswordHash != hashedPassword) return Result<UserDto>.Failure("Şifre hatalı.");

            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto, "Giriş başarılı!");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
