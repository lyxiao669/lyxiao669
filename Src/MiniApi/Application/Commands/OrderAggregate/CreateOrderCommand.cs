using MediatR;

namespace MiniApi.Application
{
    public class CreateOrderCommand : IRequest<CreateOrderResult>
    {
        public int SpotId { get; set; }
    }

    public class CreateOrderResult
    {
        public int OrderId { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
