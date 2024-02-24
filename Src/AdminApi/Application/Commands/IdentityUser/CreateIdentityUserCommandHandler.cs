using Domain.Aggregates;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateIdentityUserCommandHandler : IRequestHandler<CreateIdentityUserCommand, bool>
    {
        readonly IIdentityUserRepository _identityUserRepository;
        readonly IRandomService _randomService;
        readonly ApplicationDbContext _context;
        readonly IMediator _mediator;

        public CreateIdentityUserCommandHandler(IIdentityUserRepository identityUserRepository, IRandomService randomService, ApplicationDbContext context, IMediator mediator)
        {
            _identityUserRepository = identityUserRepository;
            _randomService = randomService;
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var user=new IdentityUser(
                fullName: request.FullName,
                mobile: request.Mobile,
                school: request.School,
                grade: request.Grade,
                age: request.Age,
                gender: request.Gender);

            var qRCode = string.Empty;
            for (int i = 0; i < 1000; i++)
            {
                qRCode = _randomService.RandomNumber(9);
                var exists = await _context.IdentityUsers.Where(a => a.QRCode == qRCode).AnyAsync();

                if (!exists)
                {
                    break;
                }
            }

            user.Update(qRCode);

            _identityUserRepository.Add(user);

            return await _identityUserRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
