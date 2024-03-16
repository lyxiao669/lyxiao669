using System;
using MediatR;

namespace MiniApi.Application
{
  public class FavoriteDetailResult
  {
    public int FavoriteId { get; set; }

    public string UserName { get; set; }
    public string Avatar { get; set; }
    
    public string SpotName { get; set; }
    public string ProvinceName { get; set; }
    public string CityName { get; set; }
    public int Likes { get; set; }
    public string Description { get; set; }
    public decimal TicketPrice { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Images { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string OpeningHours { get; set; }
    public DateTime Timestamp { get; set; }
  }

}
