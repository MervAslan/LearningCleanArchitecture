using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.CQRS.Commands.UserCommands
{
    public class RegisterUserCommand : IRequest<Result<UserDto>>
    {
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null) return Result<UserDto>.Failure("Kullanıcı adı zaten kayıtlı.");
            
            existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null) return Result<UserDto>.Failure("Email zaten kayıtlı.");
            
            var user = _mapper.Map<User>(request);
            user.PasswordHash = HashPassword(request.Password);

            await _userRepository.AddAync(user);
            await _userRepository.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto, "Kayıt başarılı!");
        }
        private string HashPassword(string password)
        {
            // basit hash yaptım
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
