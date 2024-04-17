using Applet.API.Infrastructure;
using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MiniApi.Application.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class FeedbackQueries : BaseQueries
    {
        private readonly ApplicationDbContext _context;
        readonly UsersAccessor _usersAccessor;

        public FeedbackQueries(ApplicationDbContext context, UsersAccessor usersAccessor)
        {
            _context = context;
            _usersAccessor = usersAccessor;
        }

        /// <summary>
        /// 根据用户ID查询反馈
        /// </summary>
        /// <returns>反馈详情列表</returns>
        public async Task<List<FeedbackDetailResult>> GetFeedbackDetailsByUserIdAsync()
        {
            var userId = _usersAccessor.Id;
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                // 如果找不到用户，返回空列表
                return new List<FeedbackDetailResult>();
            }

            // 获取该用户的所有反馈
            var feedbacks = await _context.Feedback
                .Where(f => f.UserId == userId)
                .ToListAsync();

            var feedbackDetailsList = feedbacks.Select(f => new FeedbackDetailResult
            {
                FeedbackId = f.Id,
                UserId = f.UserId,
                UserName = user.UserName,
                Phone = f.Phone,
                Content = f.Content,
                Status = f.Status,
                CreateDate = f.CreateDate
            }).ToList();

            return feedbackDetailsList;
        }
    }
}
