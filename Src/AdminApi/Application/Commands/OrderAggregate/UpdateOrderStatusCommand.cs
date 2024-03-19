using MediatR;

namespace AdminApi.Application.Commands
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
    }
}
