

using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    /// <summary>
    /// 导出二维码图片压缩包
    /// </summary>
    public class QrCodeQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;
        readonly IMediator _mediator;

        public QrCodeQueries(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public Stream GetQrCodeZipAsync(QrCodeModel model)
        {

            var Data=new
            {

            }.ToJson();

            StringContent stringContent = new StringContent(Data);
            stringContent.Headers.ContentType.MediaType = "application/json";
            var httpClient=new HttpClient();
            var response = httpClient.PostAsync("http://116.62.21.132:8405/QrCodes", stringContent);

            var query = _context.IdentityUsers
                .WhereIF(a => a.School.Contains(model.School), model.School != null)
                .WhereIF(a => a.Grade.Contains(model.Grade), model.Grade != null);

            var list =  query
                .ToList();

            var imgList = new List<string>();

            foreach (var user in list)
            {
                var s = Guid.NewGuid().ToString();
                imgList.Add(s+"/"+user.FullName+","+user.QrCodeImg);
            }
            var ms = ZipUtil.Download(imgList);
            return ms;
            



            #region
            //string zipName = string.Empty;

            //if(model.Grade != null && model.School != null)
            //{
            //    zipName = model.School + "(" + model.Grade + ")的" + "二维码.zip";
            //}
            //else
            //{
            //    zipName ="所有"+ "二维码.zip";
            //}

            //string result="压缩包文件地址:"+ "C:\\FileZip\\" + zipName;
            //ZipUtil.SaveFile(ms, zipName);

            //return result;
            #endregion
        }
    }
}
