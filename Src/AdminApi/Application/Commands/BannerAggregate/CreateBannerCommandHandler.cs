using Juzhen.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, bool>
    {
        readonly IBannerRepository _bannerRepository;

        public CreateBannerCommandHandler(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<bool> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {

            var banner = new Banner(request.Img, request.Sort);
            _bannerRepository.Add(banner);
            await _bannerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;

        }
    }
}
