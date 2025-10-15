using AutoMapper;
using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;

namespace ToDo.Application.CQRS.Queries.DashboardQueries
{
    public class DashboardQuery
    {
        public class GetUserDashboardQuery : IRequest<Result<UserDto>>
        {
           
        }

        public class GetUserDashboardQueryHandler : IRequestHandler<GetUserDashboardQuery, Result<UserDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ICurrentUserService _currentUserService;

            public GetUserDashboardQueryHandler(IUserRepository userRepository, IMapper mapper, ICurrentUserService currentUserService)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _currentUserService = currentUserService;
            }

            public async Task<Result<UserDto>> Handle(GetUserDashboardQuery request, CancellationToken cancellationToken)
            {
                var currentUserId = _currentUserService.CurrentUserId;
                var user = await _userRepository.GetUserWithDetailsAsync(currentUserId);
                if (user == null) return Result<UserDto>.Failure("Kullanıcı bulunamadı.");

                var userDto = _mapper.Map<UserDto>(user);
                return Result<UserDto>.Success(userDto);
            }
        }
    }
}
