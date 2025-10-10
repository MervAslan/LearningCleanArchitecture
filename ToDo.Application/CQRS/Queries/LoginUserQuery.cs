using MediatR;
using System.ComponentModel.DataAnnotations;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;

namespace ToDo.Application.CQRS.Queries
{
    public class LoginUserQuery : IRequest<Result<bool>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<bool>>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null) return Result<bool>.Failure("Kullanıcı bulunamadı.");

            var hashedPassword = HashPassword(request.Password);
            if (user.PasswordHash != hashedPassword) return Result<bool>.Failure("Şifre hatalı.");

            return Result<bool>.Success(true, "Giriş başarılı!");
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
