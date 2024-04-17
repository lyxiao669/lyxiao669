using Applet.API.Infrastructure;
using Domain.Aggregates;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, CreateFeedbackResult>
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly UsersAccessor _usersAccessor;

        public CreateFeedbackCommandHandler(IFeedbackRepository feedbackRepository, UsersAccessor usersAccessor)
        {
            _feedbackRepository = feedbackRepository;
            _usersAccessor = usersAccessor;
        }

        public async Task<CreateFeedbackResult> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var userId = _usersAccessor.Id; // 获取当前用户
            var feedback = new Feedback
            {
                UserId = _usersAccessor.Id,
                Phone = request.Phone,
                Content = request.Content,
                Status = 0,
                CreateDate = DateTime.Now
            };

            // 添加反馈到仓库
            _feedbackRepository.Add(feedback);
            await _feedbackRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new CreateFeedbackResult
            {
                FeedbackId = feedback.Id,
                Success = true,
                Message = "反馈已成功提交"
            };
        }
    }
}
