using AutoMapper;
using Domain.Aggregates;
using System.Collections.Generic;

namespace MiniApi.Application
{
    public static class AllMapper
    {
        internal static IMapper Mapper { get; }

        static AllMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AllProfile>())
                .CreateMapper();
        }

        public static QuestionListResult Map(this List<Question> questions)
        {
            return Mapper.Map<QuestionListResult>(questions);
        }

        public static QuestionOptionsDetailResult OptionsMap(this QuestionOptions questionOptions)
        {
            return Mapper.Map<QuestionOptionsDetailResult>(questionOptions);
        }

        public static UserInformationResult Map(this IdentityUser user)
        {
            return Mapper.Map<UserInformationResult>(user);
        }

        public static PhotoResult PhotoMap(this IdentityUser user)
        {
            return Mapper.Map<PhotoResult>(user);
        }
        public static PhotoListResult PhotoListMap(this List<IdentityUser> users)
        {
            return Mapper.Map<PhotoListResult>(users);
        }


        public static AnswerResultRecordResultList Map(this List<AnswerResultRecord> records)
        {
            return Mapper.Map<AnswerResultRecordResultList>(records);
        }

        public static QuestionBankDetailResult BankMap(this QuestionBank bank)
        {
            return Mapper.Map<QuestionBankDetailResult>(bank);
        }
    }
}
