using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure
{
    public class MD5Helper
    {
        public static string Md5_32BitHash(string text, bool lower = true)
        {
            using var cryp = MD5.Create();
            var data = cryp.ComputeHash(Encoding.UTF8.GetBytes(text));
            var crypdata = string.Join("", data.Select(s => s.ToString(lower ? "x2" : "X2")));
            return crypdata;
        }
    }
}
