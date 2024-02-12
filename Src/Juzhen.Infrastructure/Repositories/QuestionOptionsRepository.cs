using Juzhen.Domain.Aggregates;
using Juzhen.Domain.Aggregates.QuestionOptionsAggregate;
using Juzhen.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure.Repositories
{
    public class QuestionOptionsRepository : IQuestionOptionsRepository
    {
        readonly ApplicationDbContext _context;

        public QuestionOptionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(QuestionOptions options)
        {
            _context.QuestionOptions.Add(options);
        }

        public void Delete(QuestionOptions options)
        {
            _context.QuestionOptions.Remove(options);
        }

        public async Task<QuestionOptions> GetAsync(int id)
        {
            return await _context.QuestionOptions
                .FindAsync(id);
        }


        public async Task<List<QuestionOptions>> GetByQuestionIdListAsync(int questionId)
        {
            return await _context.QuestionOptions
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();
        }

        public async Task<List<QuestionOptions>> GetByQuestionIdRightListAsync(int questionId)
        {
            return await _context.QuestionOptions
                .Where(a => a.QuestionId == questionId)
                .Where(a=>a.IsAnswer==true)
                .ToListAsync();
        }

        public void Update(QuestionOptions options)
        {
            _context.QuestionOptions.Update(options);
        }
    }
}
