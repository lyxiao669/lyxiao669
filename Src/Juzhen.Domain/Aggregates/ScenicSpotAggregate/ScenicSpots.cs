using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Juzhen.Domain.SeedWork;

namespace Juzhen.Domain.Aggregates
{
    [Table("ScenicSpots")]
    public class ScenicSpots: IAggregateRoot
    {
       
        public int Id { get; set; }

        public string SpotName { get; set; }

        public string ProvinceName { get; set; }

        public string CityName { get; set; }

        public string Description { get; set; }

        public decimal TicketPrice { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Images { get; set; }
        public int Likes { get; set; }

        public ScenicSpots(){}

        public ScenicSpots(string spotName, string provinceName, string cityName, string description, decimal ticketPrice, double latitude, double longitude, string images) :this()
        {
            SpotName = spotName;
            ProvinceName = provinceName;
            CityName = cityName;
            Description = description;
            TicketPrice = ticketPrice;
            Latitude = latitude;
            Longitude = longitude;
            Images = images;
        }
        public void update(string spotName,string provinceName, string cityName, string description, decimal ticketPrice, double latitude, double longitude, string images)
        {
            SpotName = spotName;
            ProvinceName = provinceName;
            CityName = cityName;
            Description = description;
            TicketPrice = ticketPrice;
            Latitude = latitude;
            Longitude = longitude;
            Images = images;
        }

    }
}
