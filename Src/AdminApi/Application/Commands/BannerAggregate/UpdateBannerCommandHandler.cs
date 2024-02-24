using Domain.Aggregates;
using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, bool>
    {
        readonly IBannerRepository _bannerRepository;

        public UpdateBannerCommandHandler(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<bool> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            var banners = await _bannerRepository.GetAsync(request.Id);

            banners.Update(request.Img, request.Sort);

            if (banners == null)
            {
                throw new NotFoundException($"{request.Id}");
            }
            _bannerRepository.Update(banners);
            return true;
        }
    }
}
