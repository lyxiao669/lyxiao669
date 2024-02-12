using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    /// <summary>
    /// 轮播图
    /// </summary>
    [Table("banner")]
    public class Banner:AggregateRoot
    {
        /// <summary>
        /// 并发控制
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; private set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; private set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; private set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; private set; }=DateTime.Now;

        public Banner()
        {

        }

        public Banner(string img, int sort)
        {
            Img = img;
            Sort = sort;
        }

        public void Update(string img, int sort)
        {
            Img = img;
            Sort = sort;
        }
    }
}
