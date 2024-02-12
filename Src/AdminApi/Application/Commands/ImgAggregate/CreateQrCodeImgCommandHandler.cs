using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructur;
using Juzhen.Qiniu.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateQrCodeImgCommandHandler : IRequestHandler<CreateQrCodeImgCommand, bool>
    {
        readonly IIdentityUserRepository _identityUserRepository;

        public CreateQrCodeImgCommandHandler(IIdentityUserRepository identityUserRepository)
        {
            _identityUserRepository = identityUserRepository;
        }

        public async Task<bool> Handle(CreateQrCodeImgCommand request, CancellationToken cancellationToken)
        {
            var userList=await _identityUserRepository.GetNotQrCodeImgList();
            foreach (var user in userList)
            {
                var s= ImgUtil.UploadQrCode(user.QRCode, user.FullName);

                var qiniufile = QiniuUtil.GetUploadFile(s);

                user.AddQrCodeImg(qiniufile);

                _identityUserRepository.Update(user);
            }

            return await _identityUserRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }

    }
}
