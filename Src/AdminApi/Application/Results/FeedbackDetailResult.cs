using System;

public class FeedbackDetailResult
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Phone { get; set; }
    public string Content { get; set; }
    public int Status { get; set; }
    public DateTime CreateDate { get; set; }
}
