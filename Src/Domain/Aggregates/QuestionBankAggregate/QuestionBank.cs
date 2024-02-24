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
    /// 题库问题
    /// </summary>
    [Table("question_bank")]
    public class QuestionBank:AggregateRoot
    {
        [Timestamp]
        public byte [] Timestamp { get;private set; }


        /// <summary>
        /// 题库标题
        /// </summary>
        public string Title { get;private set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get; private set; }

        /// <summary>
        /// 时间限制
        /// </summary>
        public int TimeLimit { get; private set; }

        /// <summary>
        /// 题目数量
        /// </summary>
        public int Amount { get; private set; }



        public DateTime CreateTime { get; private set; }=DateTime.Now;

        public QuestionBank()
        {

        }

        public QuestionBank(string title, string grade, int timeLimit, int amount) : this()
        {
            Title = title;
            Grade = grade;
            TimeLimit = timeLimit;
            Amount = amount;
        }

        public void Update(string title, string grade, int timeLimit, int amount)
        {
            Title = title;
            Grade = grade;
            TimeLimit = timeLimit;
            Amount = amount;
        }


    }
}
