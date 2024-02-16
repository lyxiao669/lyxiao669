using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, bool>
    {
        readonly IUsersRepository _userRepository;

        public CreateUsersCommandHandler(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var user = new Users(request.UserName, request.Password, request.Avatar);
            _userRepository. Add(user);
            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
