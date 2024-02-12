using MediatR;

namespace AdminApi.Application
{
    public class DeleteUserVsionCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }
}
