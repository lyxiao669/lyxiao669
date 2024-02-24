using Domain.Aggregates;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateUsersCommandHandler : IRequestHandler<UpdateUsersCommand, bool>
    {
        private readonly IUsersRepository _userRepository;

        public UpdateUsersCommandHandler(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAsync(request.Id);
            if (users == null) return false;

            users.Update(request.UserName, request.Password, request.Avatar);
            if (users == null)
            {
                throw new NotFoundException($"{request.Id}");
            }
            _userRepository.Update(users);
            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
