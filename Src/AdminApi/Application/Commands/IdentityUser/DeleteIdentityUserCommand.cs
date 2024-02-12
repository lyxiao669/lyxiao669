using MediatR;

namespace AdminApi.Application
{
    public class DeleteIdentityUserCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }
}
