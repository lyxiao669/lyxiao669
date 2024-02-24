using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class QuestionBankQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public QuestionBankQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 题库详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<QuestionBank> GetQuestionBankDetailAsync(int id)
        {
            var result=await _context.QuestionBanks
                .Where(a=>a.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// 题库问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageResult<QuestionBank>> GetQuestionBankListAsync(PageModel model)
        {
            var query =  _context.QuestionBanks;

            var list = await query
                .OrderByDescending(a => a.Id)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return PageResult(list, count);
        }
    }
}
