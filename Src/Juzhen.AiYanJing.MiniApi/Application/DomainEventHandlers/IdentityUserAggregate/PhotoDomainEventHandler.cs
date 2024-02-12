using Juzhen.Domain.Event;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class PhotoDomainEventHandler : INotificationHandler<PhotoDomainEvent>
    {
        readonly IMediator _mediator;
        readonly IPhotoService _photoService;

        public PhotoDomainEventHandler(IMediator mediator, IPhotoService photoService)
        {
            _mediator = mediator;
            _photoService = photoService;
        }

        public async Task Handle(PhotoDomainEvent notification, CancellationToken cancellationToken)
        {
            var userId = notification.IdentityUser.Id;

            var command = new CreateCompositePhotoCommand()
            {
                UserId = userId,
            };


            _photoService.EnPhotoEnqueue(notification.IdentityUser.Photo);

            await _mediator.Send(command);
        }
    }
}
