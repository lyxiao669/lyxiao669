using Applet.API.Infrastructure;
using Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class RemoveFavoriteCommandHandler : IRequestHandler<RemoveFavoriteCommand, RemoveFavoriteResult>
    {
        private readonly IUserFavoriteRepository _favoriteRepository;
        private readonly UsersAccessor _usersAccessor;

        public RemoveFavoriteCommandHandler(IUserFavoriteRepository favoriteRepository, UsersAccessor usersAccessor)
        {
            _favoriteRepository = favoriteRepository;
            _usersAccessor = usersAccessor;
        }

        public async Task<RemoveFavoriteResult> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
        {
            var userId = _usersAccessor.Id; // 获取当前用户ID

            // 查找是否存在该收藏
            var favorite = await _favoriteRepository.GetAsync(request.SpotId);
            if (favorite == null)
            {
                return new RemoveFavoriteResult
                {
                    Success = false,
                    Message = "未找到该收藏项"
                };
            }

            // 删除收藏
            _favoriteRepository.Delete(favorite);
            await _favoriteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new RemoveFavoriteResult
            {
                Success = true,
                Message = "收藏删除成功"
            };
        }
    }
}
