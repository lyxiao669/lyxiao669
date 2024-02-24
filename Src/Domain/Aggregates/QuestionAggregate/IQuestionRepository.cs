using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public interface IQuestionRepository:IRepository<Question>
    {
        void Add(Question bank);

        void Update(Question bank);

        void Delete(Question bank);

        Task<Question> GetAsync(int id);

    }
}
