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
        private readonly IScenicSpotsRepository _scenicSpotRepository;
        private readonly UsersAccessor _usersAccessor;

        public AddToFavoritesCommandHandler(IUserFavoriteRepository favoriteRepository, IScenicSpotsRepository scenicSpotRepository, UsersAccessor usersAccessor)
        {
            _favoriteRepository = favoriteRepository;
            _scenicSpotRepository = scenicSpotRepository;
            _usersAccessor = usersAccessor;
        }

        public async Task<AddToFavoritesResult> Handle(AddToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var userId = _usersAccessor.Id; // 获取当前用户ID

            // 检查是否已收藏
            var isAlreadyFavorite = await _favoriteRepository.IsFavorite(userId, request.SpotId);
            if (isAlreadyFavorite)
            {
                return new AddToFavoritesResult
                {
                    Success = false,
                    Message = "您已收藏过此景点",
                };
            }

            // 获取景点对象
            var scenicSpot = await _scenicSpotRepository.GetAsync(request.SpotId);
            if (scenicSpot == null)
            {
                return new AddToFavoritesResult
                {
                    Success = false,
                    Message = "未找到该景点",
                };
            }

            // 增加景点的Likes数量
            scenicSpot.Likes += 1;
            _scenicSpotRepository.Update(scenicSpot);

            var favorite = new UserFavorite
            {
                UserId = userId,
                SpotId = request.SpotId,
                Timestamp = DateTime.Now
            };

            _favoriteRepository.Add(favorite);
            await _favoriteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            _scenicSpotRepository.Update(scenicSpot);
            await _scenicSpotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new AddToFavoritesResult
            {
                FavoriteId = favorite.Id,
                Success = true,
                Message = "收藏成功",
            };
        }


    }
}
