using MiniApi.Application;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using MiniApi.Application.Queries;

namespace MiniApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserFavoritesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserFavoriteQueries _favoriteQueries;

        public UserFavoritesController(IMediator mediator, UserFavoriteQueries favoriteQueries)
        {
            _mediator = mediator;
            _favoriteQueries = favoriteQueries;
        }

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="command">添加收藏的命令</param>
        /// <returns>添加结果</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(AddToFavoritesResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddToFavorites(AddToFavoritesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// 查询用户收藏
        /// </summary>
        /// <returns>收藏列表</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<FavoriteDetailResult>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> QueryFavorites()
        {
            var result = await _favoriteQueries.GetFavoritesByUserIdAsync();
            return Ok(result);
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="id">收藏ID</param>
        /// <returns>删除结果</returns>
        [HttpDelete("{favoriteId}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteFavorite(int id)
        {
            var command = new RemoveFavoriteCommand { SpotId = id };
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok();
            }
            return Ok(result);
        }
    }
}
