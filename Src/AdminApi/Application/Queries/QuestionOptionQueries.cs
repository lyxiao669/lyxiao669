using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class QuestionOptionQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public QuestionOptionQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 问题选项详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<QuestionOptions> GetQuestionOptionDetailAsync(int id)
        {
            var result = await _context.QuestionOptions
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync(); 
            return result;
        }


        /// <summary>
        /// 问题选项列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageResult<QuestionOptions>> GetQuestionOptionListAsync(QuestionOptionModel model)
        {
            var query = _context.QuestionOptions
                .WhereIF(a => a.QuestionId==model.QuestionId, model.QuestionId != null);

            var list = await query
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return PageResult(list, count);
        }
    }
}
