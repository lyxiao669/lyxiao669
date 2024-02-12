using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using MediatR;
using System.Data;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class ExcelImportUserVsionCommandHandler : IRequestHandler<ExcelImportUserVsionCommand, bool>
    {
        readonly IUserVsionRepository _userVsionRepository;

        readonly HttpClient _httpClient;

        public ExcelImportUserVsionCommandHandler(IUserVsionRepository userVsionRepository, HttpClient httpClient)
        {
            _userVsionRepository = userVsionRepository;
            _httpClient = httpClient;
        }

        public async Task<bool> Handle(ExcelImportUserVsionCommand request, CancellationToken cancellationToken)
        {
            using (var response = await _httpClient.GetAsync(request.FilePath))
            using (var stream = response.Content.ReadAsStream())
            {
                NpoiExcelImportHelper npoiExcel = new NpoiExcelImportHelper();
                var datas = npoiExcel.ExcelToDataTableList(stream, request.FilePath, 1, out bool isSuccess, out string resultMsg);

                foreach (DataRow row in datas[0].Rows)
                {
                    var user = new UserVsion(
                        fullName: row[0].ToString().Trim(),
                        mobile: row[1].ToString().Trim(),
                        leftEyeVision: row[2].ToString().Trim(),
                        rightEyeVision: row[3].ToString().Trim(),
                        leftEyeAstigmatism: row[4].ToString().Trim(),
                        rightEyeAstigmatism:row[5].ToString().Trim(),
                        leftEyePupilDistance: row[6].ToString().Trim(),
                        rightEyePupilDistance:row[7].ToString().Trim(),
                        leftEyeAxial:row[8].ToString().Trim(),
                        rightEyeAxial:row[9].ToString().Trim(),
                        doctorAdvice:row[10].ToString().Trim()
                        );
                    _userVsionRepository.Add(user);
                }

                return await _userVsionRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
            }
        }
    }
}
