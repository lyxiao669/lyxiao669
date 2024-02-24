using Domain.Aggregates.QuestionOptionsAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class DeleteQuestionOptionsCommandHandler : IRequestHandler<DeleteQuestionOptionsCommand, bool>
    {
        readonly IQuestionOptionsRepository _questionOptionsRepository;

        public DeleteQuestionOptionsCommandHandler(IQuestionOptionsRepository questionOptionsRepository)
        {
            _questionOptionsRepository = questionOptionsRepository;
        }

        public async Task<bool> Handle(DeleteQuestionOptionsCommand request, CancellationToken cancellationToken)
        {
            var options = await _questionOptionsRepository.GetAsync(request.Id);

            _questionOptionsRepository.Delete(options);

            return await _questionOptionsRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
