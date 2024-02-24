using Domain.Aggregates;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class QuestionBankRepository : IQuestionBankRepository
    {
        readonly ApplicationDbContext _context;

        public QuestionBankRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(QuestionBank bank)
        {
            _context.Add(bank);
        }

        public void Delete(QuestionBank bank)
        {
            _context.Remove(bank);
        }

        public async Task<QuestionBank> GetAsync(int id)
        {
            return await _context.QuestionBanks.FindAsync(id);
        }

        public async Task<QuestionBank> GetByGradeAsync(string grade)
        {
            return await _context.QuestionBanks.Where(a => a.Grade == grade).FirstOrDefaultAsync();
        }

        public void Update(QuestionBank bank)
        {
            _context.Update(bank);
        }
    }
}
