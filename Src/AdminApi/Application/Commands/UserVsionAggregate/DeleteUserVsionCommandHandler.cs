using Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class DeleteUserVsionCommandHandler : IRequestHandler<DeleteUserVsionCommand, bool>
    {
        readonly IUserVsionRepository _userVsionRepository;

        public DeleteUserVsionCommandHandler(IUserVsionRepository userVsionRepository)
        {
            _userVsionRepository = userVsionRepository;
        }

        public async Task<bool> Handle(DeleteUserVsionCommand request, CancellationToken cancellationToken)
        {
            var user=await _userVsionRepository.GetAsync(request.Id);

            _userVsionRepository.Delete(user);

            return await _userVsionRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
