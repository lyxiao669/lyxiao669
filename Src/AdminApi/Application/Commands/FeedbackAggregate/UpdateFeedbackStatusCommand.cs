using MediatR;

namespace AdminApi.Application.Commands
{
    public class UpdateFeedbackStatusCommand : IRequest<bool>
    {
        public int FeedbackId { get; set; }
        public int Status { get; set; }
    }
}
