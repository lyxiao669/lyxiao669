
using Domain.Aggregates;
using System;
using System.Collections.Generic;

namespace MiniApi.Application
{
  public class FeedbackDetailResult
{
    public int FeedbackId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Phone { get; set; }
    public string Content { get; set; }
    public int Status { get; set; }
    public DateTime CreateDate { get; set; }
}

}
