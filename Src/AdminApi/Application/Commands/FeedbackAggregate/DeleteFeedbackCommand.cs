using MediatR;

namespace AdminApi.Application
{
    public class DeleteFeedbackCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
