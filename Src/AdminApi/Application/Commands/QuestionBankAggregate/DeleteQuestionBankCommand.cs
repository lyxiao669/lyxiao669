using MediatR;

namespace AdminApi.Application
{
    public class DeleteQuestionBankCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
