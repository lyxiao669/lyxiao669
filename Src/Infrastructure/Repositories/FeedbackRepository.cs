using Domain.Aggregates;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Feedback feedback)
        {
            _context.Add(feedback);
        }

        public void Delete(Feedback feedback)
        {
            _context.Remove(feedback);
        }

        public async Task<Feedback> GetAsync(int id)
        {
            return await _context.Feedback.FindAsync(id);
        }

        public void Update(Feedback feedback)
        {
            _context.Update(feedback);
        }
    }
}
