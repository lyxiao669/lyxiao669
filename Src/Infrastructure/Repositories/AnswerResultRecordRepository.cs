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
    public class AnswerResultRecordRepository : IAnswerResultRecordRepository
    {
        readonly ApplicationDbContext _context;

        public AnswerResultRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(AnswerResultRecord record)
        {
            _context.AnswerResultRecords.Add(record);
        }

        public void Delete(AnswerResultRecord record)
        {
            _context.AnswerResultRecords.Remove(record);
        }

        public async Task<List<AnswerResultRecord>> GetByUserId(int userId)
        {
            return await _context.AnswerResultRecords
                .Where(a=>a.UserId==userId)
                .ToListAsync();
        }

        public async Task<int> GetNumberByUserId(int userId)
        {
            var answer = await _context.AnswerResultRecords
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreateTime)
                .FirstOrDefaultAsync();

            if (answer == null)
            {
                return 1;
            }

            return answer.Number;
        }

        public void Update(AnswerResultRecord record)
        {
            _context.AnswerResultRecords.Update(record);
        }
    }
}
