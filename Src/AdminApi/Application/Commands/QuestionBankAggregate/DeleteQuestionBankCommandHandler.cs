using Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class DeleteQuestionBankCommandHandler : IRequestHandler<DeleteQuestionBankCommand, bool>
    {
        readonly IQuestionBankRepository _questionBankRepository;

        public DeleteQuestionBankCommandHandler(IQuestionBankRepository questionBankRepository)
        {
            _questionBankRepository = questionBankRepository;
        }

        public async Task<bool> Handle(DeleteQuestionBankCommand request, CancellationToken cancellationToken)
        {
            var bank=await _questionBankRepository.GetAsync(request.Id);

            _questionBankRepository.Delete(bank);

            return await _questionBankRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
