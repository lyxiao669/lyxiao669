using Juzhen.Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class DeleteIdentityUserCommandHandler : IRequestHandler<DeleteIdentityUserCommand, bool>
    {
        readonly IIdentityUserRepository _identityUserRepository;

        public DeleteIdentityUserCommandHandler(IIdentityUserRepository identityUserRepository)
        {
            _identityUserRepository = identityUserRepository;
        }

        public async Task<bool> Handle(DeleteIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var user=await _identityUserRepository.GetAsync(request.Id);

            _identityUserRepository.Delete(user);

            return await _identityUserRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
