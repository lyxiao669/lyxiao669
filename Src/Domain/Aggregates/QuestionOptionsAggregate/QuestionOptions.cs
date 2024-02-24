using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    /// <summary>
    /// 问题选项
    /// </summary>
    [Table("question_options")]
    public class QuestionOptions:AggregateRoot
    {

        /// <summary>
        /// 并发控制
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; private set; }

        /// <summary>
        /// 题库问题Id
        /// </summary>
        public int QuestionId { get;private set; }

        /// <summary>
        /// 选项
        /// </summary>
        public string Option { get;private set; }

        /// <summary>
        /// 选项答案内容
        /// </summary>
        public string OptionsAnswer { get;private set; }

        /// <summary>
        /// 是否是正确答案
        /// </summary>
        public bool IsAnswer { get;private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get;private set; }=DateTime.Now;

        public QuestionOptions()
        {

        }

        public QuestionOptions(int questionId, string option, string optionsAnswer, bool isAnswer) : this()
        {
            QuestionId = questionId;
            Option = option;
            OptionsAnswer = optionsAnswer;
            IsAnswer = isAnswer;
        }

        public void Update(string option, string optionsAnswer, bool isAnswer)
        {
            Option = option;
            OptionsAnswer = optionsAnswer;
            IsAnswer = isAnswer;
        }
    }
}
