using Juzhen.Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateUserVsionCommandHandler : IRequestHandler<UpdateUserVsionCommand, bool>
    {
        readonly IUserVsionRepository _userVsionRepository;

        public UpdateUserVsionCommandHandler(IUserVsionRepository userVsionRepository)
        {
            _userVsionRepository = userVsionRepository;


        }

        public async Task<bool> Handle(UpdateUserVsionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userVsionRepository.GetAsync(request.Id);

            user.Update(
                fullName: request.FullName,
                mobile: request.Mobile,
                leftEyeVision: request.LeftEyeVision,
                rightEyeVision: request.RightEyeVision,
                leftEyeAstigmatism: request.LeftEyeAstigmatism,
                rightEyeAstigmatism: request.RightEyeAstigmatism,
                leftEyePupilDistance: request.LeftEyePupilDistance,
                rightEyePupilDistance: request.RightEyePupilDistance,
                leftEyeAxial: request.LeftEyeAxial,
                rightEyeAxial: request.RightEyeAxial,
                doctorAdvice: request.DoctorAdvice
                );

            _userVsionRepository.Update(user);

            return await _userVsionRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
