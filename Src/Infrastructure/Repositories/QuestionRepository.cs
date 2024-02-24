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
    public class QuestionRepository : IQuestionRepository
    {
        readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Question bank)
        {
            _context.Questions.Add(bank);
        }

        public void Delete(Question bank)
        {
            _context.Questions.Remove(bank);
        }

        public async Task<Question> GetAsync(int id)
        {
            return await _context.Questions
                .FindAsync(id);
        }


        public void Update(Question bank)
        {
            _context.Questions.Update(bank);
        }
    }
}
