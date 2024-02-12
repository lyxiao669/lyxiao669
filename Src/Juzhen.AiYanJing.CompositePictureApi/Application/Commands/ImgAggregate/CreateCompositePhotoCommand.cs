using MediatR;

namespace Juzhen.AiYanJing.CompositePictureApi
{ 
    public class CreateCompositePhotoCommand:IRequest<bool>
    {
        public int UserId { get; set; }

        public string Photo { get; set; }
    }
}
