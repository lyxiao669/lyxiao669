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
    /// 题库问题
    /// </summary>
    [Table("question")]
    public class Question:AggregateRoot
    {
        /// <summary>
        /// 并发控制
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; private set; }

        /// <summary>
        /// 问题
        /// </summary>
        public string Problem { get;private set; }

        /// <summary>
        /// 展示图片
        /// </summary>
        public string Img { get; private set; }

        /// <summary>
        /// 答案解析
        /// </summary>
        public string AnswerAnalysis { get; private set; }



        /// <summary>
        /// 问题类型
        /// </summary>
        public QuestionType Type { get;private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get;private set; }=DateTime.Now;

        public Question()
        {

        }

        public Question(string problem, QuestionType type, string img, string answerAnalysis) : this()
        {
            Problem = problem;
            Type = type;
            Img = img;
            AnswerAnalysis = answerAnalysis;
        }

        public void Update(string problem, QuestionType type, string img, string answerAnalysis) 
        {
            Problem = problem;
            Type = type;
            Img = img;
            AnswerAnalysis = answerAnalysis;
        }

    }
}
