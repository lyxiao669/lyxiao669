using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public interface IAnswerResultRecordRepository:IRepository<AnswerResultRecord>
    {
        void Add(AnswerResultRecord record);

        void Update(AnswerResultRecord record);

        void Delete(AnswerResultRecord record);

        Task<List<AnswerResultRecord>> GetByUserId(int userId);

        Task<int> GetNumberByUserId(int userId);
    }
}
