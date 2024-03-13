using Domain.SeedWork;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Add(Order order);

        void Update(Order order);

        void Delete(Order order);

        Task<Order> GetAsync(int id);
    }
}