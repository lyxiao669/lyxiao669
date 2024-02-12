using Juzhen.Domain.Aggregates;
using System.Collections.Generic;

namespace AdminApi.Application
{
    public class QuestionResult
    {
        public int Id { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public string Problem { get;  set; }

        /// <summary>
        /// 问题类型
        /// </summary>
        public QuestionType Type { get;  set; }

        /// <summary>
        /// 问题选项
        /// </summary>
        public List<QuestionOptions> Options { get;  set; }
    }
}
