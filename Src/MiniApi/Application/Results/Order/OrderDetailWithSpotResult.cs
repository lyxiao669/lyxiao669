
using Domain.Aggregates;
using System;
using System.Collections.Generic;

namespace MiniApi.Application
{
  public class OrderDetailWithSpotResult
  {
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int Status { get; set; }

    // 用户信息
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Avatar { get; set; }

    // 景区信息
    public int SpotId { get; set; }
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
  }
}
