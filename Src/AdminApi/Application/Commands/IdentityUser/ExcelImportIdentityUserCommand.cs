using MediatR;

namespace AdminApi.Application
{
    public class ExcelImportIdentityUserCommand:IRequest<bool>
    {
        /// <summary>
        /// 文件地址
        /// </summary>

        public string FilePath { get; set; }
    }
}
