using Domain.Aggregates;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class ExcelImportIdentityUserCommandHandler : IRequestHandler<ExcelImportIdentityUserCommand, bool>
    {
        readonly IIdentityUserRepository _identityUserRepository;
        readonly HttpClient _httpClient;
        readonly ApplicationDbContext _context;
        readonly IRandomService _randomService;
        readonly IMediator _mediator;

        public ExcelImportIdentityUserCommandHandler(IIdentityUserRepository identityUserRepository, HttpClient httpClient, ApplicationDbContext context, IRandomService randomService, IMediator mediator)
        {
            _identityUserRepository = identityUserRepository;
            _httpClient = httpClient;
            _context = context;
            _randomService = randomService;
            _mediator = mediator;
        }

        public async Task<bool> Handle(ExcelImportIdentityUserCommand request, CancellationToken cancellationToken)
        {
            using (var response = await _httpClient.GetAsync(request.FilePath))
            using (var stream = response.Content.ReadAsStream())
            {
                NpoiExcelImportHelper npoiExcel = new NpoiExcelImportHelper();
                var datas = npoiExcel.ExcelToDataTableList(stream, request.FilePath, 1, out bool isSuccess, out string resultMsg);

                foreach(DataRow row in datas[0].Rows)
                {
                    var user = new IdentityUser(
                        fullName: row[0].ToString().Trim(),
                        mobile: row[1].ToString().Trim(),
                        school: row[2].ToString().Trim(),
                        grade: row[3].ToString().Trim(),
                        age: row[4].ToString().Trim(),
                        gender: row[5].ToString().Trim()
                        );

                    var qRCode = string.Empty;
                    for (int i = 0; i<1000; i++)
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
                }

                 return await _identityUserRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
            }
        }
    }
}
