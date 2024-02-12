using Juzhen.Domain.Aggregates;
using Juzhen.Domain.Aggregates.QuestionOptionsAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateQuestionOptionsCommandHandler : IRequestHandler<CreateQuestionOptionsCommand, bool>
    {
        readonly IQuestionOptionsRepository _questionOptionsRepository;

        public CreateQuestionOptionsCommandHandler(IQuestionOptionsRepository questionOptionsRepository)
        {
            _questionOptionsRepository = questionOptionsRepository;
        }

        public async Task<bool> Handle(CreateQuestionOptionsCommand request, CancellationToken cancellationToken)
        {
            var options = new QuestionOptions(
                questionId: request.QuestionId,
                option: request.Option,
                optionsAnswer: request.OptionsAnswer,
                isAnswer: request.IsAnswer
                );

            _questionOptionsRepository.Add(options);

            return await _questionOptionsRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
