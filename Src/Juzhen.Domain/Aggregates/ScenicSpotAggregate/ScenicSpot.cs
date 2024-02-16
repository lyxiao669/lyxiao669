using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juzhen.Domain.Aggregates
{
    [Table("ScenicSpots")]
    public class ScenicSpot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string SpotName { get; set; }

        [MaxLength(20)]
        public string ProvinceName { get; set; }

        [MaxLength(20)]
        public string CityName { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TicketPrice { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [MaxLength(300)]
        public string Images { get; set; }
        public ScenicSpot(){}

        public ScenicSpot(string spotName, string provinceName, string cityName, string description, decimal ticketPrice, double latitude, double longitude, string images) :this()
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
