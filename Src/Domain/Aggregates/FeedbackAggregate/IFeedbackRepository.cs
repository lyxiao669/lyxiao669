using Domain.SeedWork;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        void Add(Feedback feedback);

        void Update(Feedback feedback);

        void Delete(Feedback feedback);

        Task<Feedback> GetAsync(int id);
    }
}
