using MediatR;

namespace AdminApi.Application.Commands.ScenicSpotsAggregate
{
    public class DeleteScenicSpotCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
