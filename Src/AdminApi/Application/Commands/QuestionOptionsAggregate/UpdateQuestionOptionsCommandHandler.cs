using Domain.Aggregates.QuestionOptionsAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateQuestionOptionsCommandHandler : IRequestHandler<UpdateQuestionOptionsCommand, bool>
    {
        readonly IQuestionOptionsRepository _questionOptionsRepository;

        public UpdateQuestionOptionsCommandHandler(IQuestionOptionsRepository questionOptionsRepository)
        {
            _questionOptionsRepository = questionOptionsRepository;
        }

        public async Task<bool> Handle(UpdateQuestionOptionsCommand request, CancellationToken cancellationToken)
        {
            var options=await _questionOptionsRepository.GetAsync(request.Id);

            options.Update(
                option: request.Option,
                optionsAnswer: request.OptionsAnswer,
                isAnswer: request.IsAnswer);

            _questionOptionsRepository.Update(options);

            return await _questionOptionsRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
