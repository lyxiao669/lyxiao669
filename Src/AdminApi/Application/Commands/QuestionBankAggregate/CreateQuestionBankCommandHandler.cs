using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateQuestionBankCommandHandler : IRequestHandler<CreateQuestionBankCommand, bool>
    {
        readonly IQuestionBankRepository _questionBankRepository;

        public CreateQuestionBankCommandHandler(IQuestionBankRepository questionBankRepository)
        {
            _questionBankRepository = questionBankRepository;
        }

        public async Task<bool> Handle(CreateQuestionBankCommand request, CancellationToken cancellationToken)
        {
            var bank=new QuestionBank(
                title:request.Title,
                grade:request.Grade,
                timeLimit:request.TimeLimit,
                amount:request.Amount
                );

           

            _questionBankRepository.Add(bank);

            return await _questionBankRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
