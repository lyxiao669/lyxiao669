using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    /// <summary>
    /// 成绩结果
    /// </summary>
    [Table("achievement")]
    public class Achievement:AggregateRoot
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get;private set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Mark { get;private set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get;private set; }

        /// <summary>
        /// 第几次答题
        /// </summary>
        public int Number { get;private set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime CreateTime { get; private set; }=DateTime.Now;

        public Achievement()
        {

        }

        public Achievement(int userId, int mark, string result,int number):this()
        {
            UserId = userId;
            Mark = mark;
            Result = result;
            Number = number;
        }

        public void Update(int userId, int mark, string result)
        {
            UserId = userId;
            Mark = mark;
            Result = result;
        }
    }
}
