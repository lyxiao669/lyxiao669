using Domain.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates
{
    /// <summary>
    /// 订单
    /// </summary>
    [Table("Orders")]
    public class Order : AggregateRoot
    {
        public byte[] Timestamp { get; set; }

        public int UserId { get; set; }

        public int SpotId { get; set; }

        public int Status { get; set; }

        public DateTime OrderDate { get; set; }

        public Order() { }

        public Order(int userId, int spotId, int status)
        {
            UserId = userId;
            SpotId = spotId;
            Status = status;
            OrderDate = DateTime.Now;
        }

        public void UpdateStatus(int status)
        {
            Status = status;
        }
    }
}