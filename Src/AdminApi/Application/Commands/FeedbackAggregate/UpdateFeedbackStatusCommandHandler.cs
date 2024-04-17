using Domain.Aggregates;
using Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application.Commands
{
    public class UpdateFeedbackStatusCommandHandler : IRequestHandler<UpdateFeedbackStatusCommand, bool>
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public UpdateFeedbackStatusCommandHandler(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<bool> Handle(UpdateFeedbackStatusCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackRepository.GetAsync(request.FeedbackId);
            if (feedback == null) return false;

            feedback.Status = request.Status;
            _feedbackRepository.Update(feedback);
            await _feedbackRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
    public class UpdateResult{
        public UpdateResult(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
