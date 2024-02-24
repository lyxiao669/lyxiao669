using AutoMapper;
using Domain.Aggregates;
using System.Collections.Generic;

namespace AdminApi.Application
{
    public static class AutoMapper
    {
        internal static IMapper Mapper { get; }

        static AutoMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoProfile>())
                .CreateMapper();
        }

        public static StudentQrCodeResult Map(this List<IdentityUser> questions)
        {
            return Mapper.Map<StudentQrCodeResult>(questions);
        }
    }
}
