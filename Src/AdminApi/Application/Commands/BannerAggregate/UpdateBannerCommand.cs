using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateBannerCommand : IRequest<bool>
    {
        public int Id { get; set; }
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
