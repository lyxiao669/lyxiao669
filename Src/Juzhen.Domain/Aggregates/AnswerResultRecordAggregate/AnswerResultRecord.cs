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
    /// 用户答题记录
    /// </summary>
    [Table("answer_result_record")]
    public class AnswerResultRecord:AggregateRoot
    {

        /// <summary>
        /// 并发控制
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; private set; }


        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get;private set; }


        /// <summary>
        /// 问题Id
        /// </summary>
        public int QuestionId { get; private set; }

        /// <summary>
        /// 选择的问题选项
        /// </summary>
        public string QuestionOption { get; private set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public bool IsTrue { get; private set; }


        /// <summary>
        /// 第几次答题
        /// </summary>
        public int Number { get; private set; } = 0;


        /// <summary>
        /// 答题时间
        /// </summary>
        public DateTime CreateTime { get; private set; }=DateTime.Now;


        public AnswerResultRecord()
        {

        }

        public AnswerResultRecord(int userId, int questionId, string questionOption)
        {
            UserId = userId;
            QuestionId = questionId;
            QuestionOption = questionOption;
        }


        public void AddNumber(int number)
        {
            Number = number;
        }

        public void Result(bool istrue)
        {
            IsTrue = istrue;
        }
    }
}
