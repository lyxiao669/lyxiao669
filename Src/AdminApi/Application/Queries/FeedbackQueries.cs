using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application.Queries
{
    public class FeedbackQueries : BaseQueries
    {
        private readonly ApplicationDbContext _context;

        public FeedbackQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 查询反馈列表，按创建时间降序
        /// </summary>
        /// <param name="model">分页模型</param>
        /// <returns>反馈列表</returns>
        public async Task<PageResult<FeedbackDetailResult>> GetFeedbackListAsync(FeedbackModel model)
        {
            var query = from feedback in _context.Feedback
                        join user in _context.Users on feedback.UserId equals user.Id
                        where feedback.Status == model.Status || model.Status == -1
                        orderby feedback.CreateDate descending
                        select new FeedbackDetailResult
                        {
                            Id = feedback.Id,
                            UserId = user.Id,
                            UserName = user.UserName,
                            Phone = feedback.Phone,
                            Content = feedback.Content,
                            Status = feedback.Status,
                            CreateDate = feedback.CreateDate
                        };

            var list = await query
                .Skip((model.PageIndex - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return PageResult(list, count);
        }
    }
}
