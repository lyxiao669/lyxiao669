using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class QuestionQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public QuestionQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 问题列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageResult<Question>> GetQuestionListAsync(QuestionModel model)
        {
            var query = _context.Questions
                .WhereIF(a => a.Problem.Contains(model.KeyWord), model.KeyWord != null);

            var list=await query
                .OrderByDescending(a => a.Id)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return PageResult(list, count);
        }

        /// <summary>
        /// 问题详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<QuestionResult> GetQuestionDetailAsync(int id)
        {
            var result=new QuestionResult();

            var question = await _context.Questions
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if(question == null)
            {
                return null;
            }
            var option = await _context.QuestionOptions.Where(a => a.QuestionId == question.Id)
                .ToListAsync();

            result = new QuestionResult
            {
                Id = id,
                Problem = question.Problem,
                Type = question.Type,
                Options = option
            };
            return result;
        }
    }
}
