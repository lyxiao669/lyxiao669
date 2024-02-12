using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructur;
using Juzhen.Qiniu.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class CreateCompositePhotoCommandHandler : IRequestHandler<CreateCompositePhotoCommand, bool>
    {
        readonly IIdentityUserRepository _identityUserRepository;

        public CreateCompositePhotoCommandHandler(IIdentityUserRepository identityUserRepository)
        {
            _identityUserRepository = identityUserRepository;
        }

        public async Task<bool> Handle(CreateCompositePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityUserRepository.GetAsync(request.UserId);

            var s= ImgUtil.UploadImg(user.QRCode, "carBackground.png", user.Photo);


            var qiniufile = QiniuUtil.GetUploadFile(s);

            user.CompositePhotos(qiniufile);

            _identityUserRepository.Update(user);

            return await _identityUserRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
