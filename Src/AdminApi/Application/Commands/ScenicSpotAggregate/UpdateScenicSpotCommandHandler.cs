using Domain.Aggregates;
using Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application.Commands.ScenicSpotsAggregate
{
    public class UpdateScenicSpotCommandHandler : IRequestHandler<UpdateScenicSpotCommand, bool>
    {
        private readonly IScenicSpotsRepository _scenicSpotRepository;

        public UpdateScenicSpotCommandHandler(IScenicSpotsRepository scenicSpotRepository)
        {
            _scenicSpotRepository = scenicSpotRepository;
        }

        public async Task<bool> Handle(UpdateScenicSpotCommand request, CancellationToken cancellationToken)
        {
            var scenicSpot = await _scenicSpotRepository.GetAsync(request.Id);
            if (scenicSpot == null) return false;

            scenicSpot.SpotName = request.SpotName;
            scenicSpot.ProvinceName = request.ProvinceName;
            scenicSpot.CityName = request.CityName;
            scenicSpot.Likes = request.Likes;
            scenicSpot.Description = request.Description;
            scenicSpot.TicketPrice = request.TicketPrice;
            scenicSpot.Latitude = request.Latitude;
            scenicSpot.Longitude = request.Longitude;
            scenicSpot.Images = request.Images;
            scenicSpot.Address = request.Address;
            scenicSpot.Telephone = request.Telephone;
            scenicSpot.OpeningHours = request.OpeningHours;
            
            _scenicSpotRepository.Update(scenicSpot);
            await _scenicSpotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
