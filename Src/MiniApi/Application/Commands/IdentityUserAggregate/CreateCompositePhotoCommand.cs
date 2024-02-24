using MediatR;

namespace MiniApi.Application
{ 
    public class CreateCompositePhotoCommand:IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
