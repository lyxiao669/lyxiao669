using MediatR;

namespace MiniApi.Application
{
    public class CancelOrderCommand : IRequest<CanceleOrderResult>
    {
        public int OrderId { get; set; }
    }

    public class CanceleOrderResult
    {
        // public int OrderId { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
