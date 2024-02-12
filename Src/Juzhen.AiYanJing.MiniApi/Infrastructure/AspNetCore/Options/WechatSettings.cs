using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tanzhongbao.webapi.Infrastructure.AspNetCore.Options
{
    public class WechatSettings
    {
        public string MchId { get; set; }
        public string CertSerialNo { get; set; }
        public string CertPrivateKey { get; set; }

        public string AppId { get; set; }
    }
}
