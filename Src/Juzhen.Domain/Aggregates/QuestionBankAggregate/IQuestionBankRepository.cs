using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    public interface IQuestionBankRepository:IRepository<QuestionBank>
    {
        void Add(QuestionBank bank);

        void Update(QuestionBank bank);

        void Delete(QuestionBank bank);

        Task<QuestionBank> GetAsync(int id);

        Task<QuestionBank> GetByGradeAsync(string grade);
    }
}
