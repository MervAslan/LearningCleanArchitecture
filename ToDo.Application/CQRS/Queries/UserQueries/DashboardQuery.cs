using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;
using ToDo.Application.Helpers;
using ToDo.Application.Interfaces;

namespace ToDo.Application.CQRS.Queries.UserQueries
{
    public class DashboardQuery
    {
        public class GetUserDashboardQuery : IRequest<Result<UserDto>>
        {
            public string Email { get; set; } //bunu sessiondan alıcaz
        }

        public class GetUserDashboardQueryHandler : IRequestHandler<GetUserDashboardQuery, Result<UserDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUserDashboardQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<Result<UserDto>> Handle(GetUserDashboardQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserWithDetailsAsync(request.Email);
                if (user == null) return Result<UserDto>.Failure("Kullanıcı bulunamadı.");

                var userDto = _mapper.Map<UserDto>(user);
                return Result<UserDto>.Success(userDto);
            }
        }
    }
}
