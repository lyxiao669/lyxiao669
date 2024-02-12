using MediatR;

namespace AdminApi.Application
{
    public class DeleteQuestionCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
