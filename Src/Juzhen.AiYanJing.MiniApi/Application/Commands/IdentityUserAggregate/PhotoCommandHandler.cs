
using Applet.API.Infrastructure;
using Juzhen.Domain.Aggregates;
using Juzhen.Qiniu.Infrastructure;
using Mammothcode.Library.Data;
using MediatR;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class PhotoCommandHandler : IRequestHandler<PhotoCommand, bool>
    {
        readonly IIdentityUserRepository _identityUserRepository;
        readonly UserAccessor _userAccessor;

        public PhotoCommandHandler(IIdentityUserRepository identityUserRepository, UserAccessor userAccessor)
        {
            _identityUserRepository = identityUserRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(PhotoCommand request, CancellationToken cancellationToken)
        {
            var user=await _identityUserRepository.GetAsync(_userAccessor.Id);

            user.GeneratePhotos(request.Photo);

            _identityUserRepository.Update(user);
            var Data = new
            {
                userId=user.Id,
                photo=request.Photo,
            }.ToJson();

            StringContent stringContent = new StringContent(Data);
            stringContent.Headers.ContentType.MediaType = "application/json";
            var httpClient = new HttpClient();
            var response = httpClient.PostAsync("http://116.62.21.132:8405/QrCodes/photo", stringContent);

            return await _identityUserRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
