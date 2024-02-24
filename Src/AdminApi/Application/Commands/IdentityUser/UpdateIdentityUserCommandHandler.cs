using Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateIdentityUserCommandHandler : IRequestHandler<UpdateIdentityUserCommand, bool>
    {
        readonly IIdentityUserRepository _identityUserRepository;

        public UpdateIdentityUserCommandHandler(IIdentityUserRepository identityUserRepository)
        {
            _identityUserRepository = identityUserRepository;
        }

        public async Task<bool> Handle(UpdateIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityUserRepository.GetAsync(request.Id);

            user.Update(
                fullName: request.FullName,
                mobile: request.Mobile,
                school: request.School,
                grade: request.Grade,
                age: request.Age,
                gender: request.Gender);

           _identityUserRepository.Update(user);

            return await _identityUserRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
