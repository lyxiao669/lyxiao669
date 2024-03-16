using MediatR;

namespace MiniApi.Application
{

  public class AddToFavoritesCommand: IRequest<AddToFavoritesResult>
  {
    public int Id { get; set; }
  }
  public class AddToFavoritesResult
  {
    public int FavoriteId { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
  }

}
