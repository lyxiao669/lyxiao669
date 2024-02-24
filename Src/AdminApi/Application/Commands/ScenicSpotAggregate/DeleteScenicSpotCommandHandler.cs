using Domain.Aggregates;
using Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application.Commands.ScenicSpotsAggregate
{
    public class DeleteScenicSpotCommandHandler : IRequestHandler<DeleteScenicSpotCommand, bool>
    {
        private readonly IScenicSpotsRepository _scenicSpotRepository;

        public DeleteScenicSpotCommandHandler(IScenicSpotsRepository scenicSpotRepository)
        {
            _scenicSpotRepository = scenicSpotRepository;
        }

        public async Task<bool> Handle(DeleteScenicSpotCommand request, CancellationToken cancellationToken)
        {
            var user = await _scenicSpotRepository.GetAsync(request.Id);
            _scenicSpotRepository.Delete(user);
            await _scenicSpotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
