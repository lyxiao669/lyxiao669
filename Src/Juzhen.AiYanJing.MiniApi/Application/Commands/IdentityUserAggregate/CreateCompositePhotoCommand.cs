using MediatR;

namespace Juzhen.AiYanJing.MiniApi.Application
{ 
    public class CreateCompositePhotoCommand:IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
