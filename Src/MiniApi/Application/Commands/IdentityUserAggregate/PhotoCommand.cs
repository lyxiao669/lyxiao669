using MediatR;

namespace MiniApi.Application
{
    public class PhotoCommand:IRequest<bool>
    {

        public string Photo { get; set; }
    }
}
