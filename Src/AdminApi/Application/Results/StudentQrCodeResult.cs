using System.Collections.Generic;

namespace AdminApi.Application
{
    public class StudentQrCodeResult
    {
        public List<IdentityUserDto> Data { get; set; }=new List<IdentityUserDto>();

        public StudentQrCodeResult()
        {

        }

        public StudentQrCodeResult(List<IdentityUserDto> data)
        {
            Data = data;
        }

        public class IdentityUserDto
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string FullName { get; set; }

            /// <summary>
            /// 二维码
            /// </summary>
            public string QRcode { get; set; }
        }
    }
}
