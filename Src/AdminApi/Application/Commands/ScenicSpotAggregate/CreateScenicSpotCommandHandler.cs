using Domain.Aggregates;
using Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application.Commands.ScenicSpotsAggregate
{
    public class CreateScenicSpotCommandHandler : IRequestHandler<CreateScenicSpotCommand, bool>
    {
        readonly IScenicSpotsRepository _scenicSpotRepository;

        public CreateScenicSpotCommandHandler(IScenicSpotsRepository scenicSpotRepository)
        {
            _scenicSpotRepository = scenicSpotRepository;
        }

        public async Task<bool> Handle(CreateScenicSpotCommand request, CancellationToken cancellationToken)
        {
            var scenicSpot = new ScenicSpots
            {
                SpotName = request.SpotName,
                ProvinceName = request.ProvinceName,
                CityName = request.CityName,
                Description = request.Description,
                TicketPrice = request.TicketPrice,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Images = request.Images,
                Likes = request.Likes,
                Address = request.Address,
                Telephone = request.Telephone,
                OpeningHours = request.OpeningHours,
                
            };
            _scenicSpotRepository.Add(scenicSpot);
            await _scenicSpotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
