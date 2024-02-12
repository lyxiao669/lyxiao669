using AutoMapper;
using Juzhen.Domain.Aggregates;
using System.Collections.Generic;

namespace AdminApi.Application
{
    public class AutoProfile:Profile
    {
        public AutoProfile()
        {
            CreateMap<IdentityUser, StudentQrCodeResult.IdentityUserDto>();

            CreateMap<List<IdentityUser>, StudentQrCodeResult>()
                .ForMember(dest => dest.Data, source => source.MapFrom(a => a));
        }
    }
}
