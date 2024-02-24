using System.Collections.Generic;

namespace MiniApi.Application
{
    public class PhotoListResult
    {
        public List<IdentityUserDto> Data { get; set; }=new List<IdentityUserDto>();

        public PhotoListResult()
        {

        }

        public PhotoListResult(List<IdentityUserDto> data)
        {
            Data = data;
        }

        public class IdentityUserDto
        {
            /// <summary>
            /// 照片
            /// </summary>
            public string Photo { get; set; }
        }
    }
}
