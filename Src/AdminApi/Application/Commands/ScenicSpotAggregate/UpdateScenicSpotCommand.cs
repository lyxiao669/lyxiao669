using MediatR;

namespace AdminApi.Application.Commands.ScenicSpotsAggregate
{
    public class UpdateScenicSpotCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string SpotName { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
        public decimal TicketPrice { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Images { get; set; }
    }
}
