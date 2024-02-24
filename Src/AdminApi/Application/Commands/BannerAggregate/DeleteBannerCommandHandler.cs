using Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand, bool>
    {
        readonly IBannerRepository _bannerRepository;

        public DeleteBannerCommandHandler(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<bool> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
        {
            var banner = await _bannerRepository.GetAsync(request.Id);
             _bannerRepository.Delete(banner);
            await _bannerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
