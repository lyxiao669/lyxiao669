using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateBannerCommand: IRequest<bool>
    {
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get;  set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get;  set; }
    }
}
