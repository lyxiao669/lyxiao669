using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class DeleteBannerCommand: IRequest<bool>
    {
        public int Id { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get;  set; }
    }
}
