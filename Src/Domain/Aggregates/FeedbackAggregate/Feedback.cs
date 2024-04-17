using Domain.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates
{
    /// <summary>
    /// 用户反馈
    /// </summary>
    [Table("Feedback")]
    public class Feedback : IAggregateRoot
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Phone { get; set; }

        public string Content { get; set; }
        public int Status { get; set; }

        public DateTime CreateDate { get; set; }

        public Feedback() { }

        public Feedback(int userId, string phone, string content, int status)
        {
            UserId = userId;
            Phone = phone;
            Content = content;
            Status = status;
            CreateDate = DateTime.Now;
        }
    }
}
