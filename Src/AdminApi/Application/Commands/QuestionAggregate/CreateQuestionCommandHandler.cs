using Juzhen.Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, bool>
    {
        readonly IQuestionRepository _questionRepository;

        public CreateQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<bool> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question(
                problem:request.Problem,
                type:request.Type,
                img:request.Img,
                answerAnalysis:request.AnswerAnalysis
                );


            _questionRepository.Add(question);

            return await _questionRepository.UnitOfWork.SaveChangeAsync(cancellationToken);

        }
    }
}
