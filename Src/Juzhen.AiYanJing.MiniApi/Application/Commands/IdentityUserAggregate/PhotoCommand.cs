using MediatR;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class PhotoCommand:IRequest<bool>
    {

        public string Photo { get; set; }
    }
}
