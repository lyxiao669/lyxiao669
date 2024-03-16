using Applet.API.Infrastructure;
using Domain.Aggregates;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class AddToFavoritesCommandHandler : IRequestHandler<AddToFavoritesCommand, AddToFavoritesResult>
    {
        private readonly IUserFavoriteRepository _favoriteRepository;
        private readonly UsersAccessor _usersAccessor;

        public AddToFavoritesCommandHandler(IUserFavoriteRepository favoriteRepository, UsersAccessor usersAccessor)
        {
            _favoriteRepository = favoriteRepository;
            _usersAccessor = usersAccessor;
        }

        public async Task<AddToFavoritesResult> Handle(AddToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var userId = _usersAccessor.Id; // 获取当前用户ID

            // 检查是否已收藏
            var isAlreadyFavorite = await _favoriteRepository.IsFavorite(userId, request.Id);
            if (isAlreadyFavorite)
            {
                return new AddToFavoritesResult
                {
                    Success = false,
                    Message = "您已收藏过此景点",
                };
            }

            var favorite = new UserFavorite
            {
                UserId = userId,
                SpotId = request.Id
            };

            _favoriteRepository.Add(favorite);
            await _favoriteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new AddToFavoritesResult
            {
                FavoriteId = favorite.Id,
                Success = true,
                Message = "收藏成功",
            };
        }

    }
}
