using Applet.API.Infrastructure;
using Domain.Aggregates;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class RemoveFavoriteCommandHandler : IRequestHandler<RemoveFavoriteCommand, RemoveFavoriteResult>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserFavoriteRepository _favoriteRepository;
        private readonly IScenicSpotsRepository _scenicSpotRepository;
        private readonly UsersAccessor _usersAccessor;

        public RemoveFavoriteCommandHandler(IScenicSpotsRepository scenicSpotRepository, ApplicationDbContext context, IUserFavoriteRepository favoriteRepository, UsersAccessor usersAccessor)
        {
            _context = context;
            _favoriteRepository = favoriteRepository;
            _scenicSpotRepository = scenicSpotRepository;
            _usersAccessor = usersAccessor;
        }

        public async Task<RemoveFavoriteResult> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
        {
            var userId = _usersAccessor.Id; // 获取当前用户ID

            // 查找是否存在该收藏
            var favorite = await _context.UserFavorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.SpotId == request.SpotId);

            if (favorite == null)
            {
                return new RemoveFavoriteResult
                {
                    Success = false,
                    Message = "未找到该收藏项"
                };
            }

            // 获取景点对象
            var scenicSpot = await _scenicSpotRepository.GetAsync(request.SpotId);
            if (scenicSpot == null)
            {
                return new RemoveFavoriteResult
                {
                    Success = false,
                    Message = "未找到该景点",
                };
            }

            // 增加景点的Likes数量
            scenicSpot.Likes -= 1;
            _scenicSpotRepository.Update(scenicSpot);


            // 删除收藏
            _favoriteRepository.Delete(favorite);
            await _favoriteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            _scenicSpotRepository.Update(scenicSpot);
            await _scenicSpotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new RemoveFavoriteResult
            {
                Success = true,
                Message = "收藏删除成功"
            };
        }
    }
}
