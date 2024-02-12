using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates.QuestionOptionsAggregate
{
    public interface IQuestionOptionsRepository:IRepository<QuestionOptions>
    {
        void Add(QuestionOptions options);

        void Update(QuestionOptions options);

        void Delete(QuestionOptions options);

        Task<QuestionOptions> GetAsync(int id);

        Task<List<QuestionOptions>> GetByQuestionIdListAsync(int questionId);


        Task<List<QuestionOptions>> GetByQuestionIdRightListAsync(int questionId);
    }
}
