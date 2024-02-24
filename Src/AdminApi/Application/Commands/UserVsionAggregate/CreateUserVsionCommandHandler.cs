using Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateUserVsionCommandHandler : IRequestHandler<CreateUserVsionCommand, bool>
    {
        readonly IUserVsionRepository _userVsionRepository;

        public CreateUserVsionCommandHandler(IUserVsionRepository userVsionRepository)
        {
            _userVsionRepository = userVsionRepository;
        }

        public async Task<bool> Handle(CreateUserVsionCommand request, CancellationToken cancellationToken)
        {
            var user = new UserVsion(
                fullName: request.FullName,
                mobile: request.Mobile,
                leftEyeVision: request.LeftEyeVision,
                rightEyeVision: request.RightEyeVision,
                leftEyeAstigmatism: request.LeftEyeAstigmatism,
                rightEyeAstigmatism:request.RightEyeAstigmatism,
                leftEyePupilDistance:request.LeftEyePupilDistance,
                rightEyePupilDistance:request.RightEyePupilDistance,
                leftEyeAxial:request.LeftEyeAxial,
                rightEyeAxial:request.RightEyeAxial,
                doctorAdvice:request.DoctorAdvice
                );

            _userVsionRepository.Add(user);

            return await _userVsionRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
