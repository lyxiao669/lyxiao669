using Domain.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates
{
    /// <summary>
    /// 用户收藏
    /// </summary>
    [Table("UserFavorites")]
    public class UserFavorite : AggregateRoot
    {
        public int UserId { get; set; }

        public int SpotId { get; set; }

        public DateTime Timestamp { get; set; }

        public UserFavorite() { }

        public UserFavorite(int userId, int spotId)
        {
            UserId = userId;
            SpotId = spotId;
            Timestamp = DateTime.Now;
        }
    }
}
