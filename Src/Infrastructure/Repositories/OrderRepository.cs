using Domain.Aggregates;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Order order)
        {
            _context.Add(order);
        }

        public void Delete(Order order)
        {
            _context.Remove(order);
        }

        public async Task<Order> GetAsync(int id)
        {
            return await _context.Order.FindAsync(id);
        }

        public void Update(Order order)
        {
            _context.Update(order);
        }
    }
}