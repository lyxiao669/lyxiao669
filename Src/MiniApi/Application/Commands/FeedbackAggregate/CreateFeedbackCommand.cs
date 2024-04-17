using MediatR;

namespace MiniApi.Application
{
    public class CreateFeedbackCommand : IRequest<CreateFeedbackResult>
    {
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
    }

    public class CreateFeedbackResult
    {
        public int FeedbackId { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
