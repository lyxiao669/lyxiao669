using MediatR;

namespace AdminApi.Application
{
    public class DeleteQuestionOptionsCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }
}
