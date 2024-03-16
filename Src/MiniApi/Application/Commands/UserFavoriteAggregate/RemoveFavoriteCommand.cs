using MediatR;

namespace MiniApi.Application
{
    public class RemoveFavoriteCommand : IRequest<RemoveFavoriteResult>
    {
        public int SpotId { get; set; }
    }

    public class RemoveFavoriteResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
