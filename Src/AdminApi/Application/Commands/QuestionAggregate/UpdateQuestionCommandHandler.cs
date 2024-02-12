using Juzhen.Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, bool>
    {
        readonly IQuestionRepository _questionRepository;

        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<bool> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetAsync(request.Id);

            question.Update(
                problem: request.Problem,
                type: request.Type,
                img: request.Img,
                answerAnalysis: request.AnswerAnalysis
                );

            _questionRepository.Update(question);

            return await _questionRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
