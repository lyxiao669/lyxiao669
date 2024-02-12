using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateQuestionBankCommandHandler : IRequestHandler<UpdateQuestionBankCommand, bool>
    {
        readonly IQuestionBankRepository _questionBankRepository;

        public UpdateQuestionBankCommandHandler(IQuestionBankRepository questionBankRepository)
        {
            _questionBankRepository = questionBankRepository;
        }

        public async Task<bool> Handle(UpdateQuestionBankCommand request, CancellationToken cancellationToken)
        {
            var bank=await _questionBankRepository.GetAsync(request.Id);

            bank.Update(
                title: request.Title,
                grade: request.Grade,
                timeLimit: request.TimeLimit,
                amount: request.Amount
                );

            _questionBankRepository.Update(bank);

            return await _questionBankRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
