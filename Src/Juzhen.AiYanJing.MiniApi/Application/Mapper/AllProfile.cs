using AutoMapper;
using Juzhen.Domain.Aggregates;
using System.Collections.Generic;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class AllProfile:Profile
    {
        public AllProfile()
        {
            CreateMap<Question, QuestionListResult.QuestionDto>();

            CreateMap<List<Question>, QuestionListResult>()
                .ForMember(dest => dest.Data, source => source.MapFrom(a => a));

            CreateMap<QuestionOptions, QuestionOptionsDetailResult>();

            CreateMap<IdentityUser, UserInformationResult>();


            CreateMap<IdentityUser, PhotoResult>();

            CreateMap<IdentityUser, PhotoListResult.IdentityUserDto>();
            CreateMap <List<IdentityUser>, PhotoListResult>()
                .ForMember(dest => dest.Data, source => source.MapFrom(a => a));


            CreateMap<AnswerResultRecord, AnswerResultRecordResultList.AnswerResultRecordDto>();
            CreateMap<List<AnswerResultRecord>, AnswerResultRecordResultList>()
                .ForMember(dest => dest.Data, source => source.MapFrom(a => a));


            CreateMap<QuestionBank, QuestionBankDetailResult>();
        }
    }
}
