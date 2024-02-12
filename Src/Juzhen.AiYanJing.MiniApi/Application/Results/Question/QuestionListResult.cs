using Juzhen.Domain.Aggregates;
using System.Collections.Generic;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class QuestionListResult
    {
        public List<QuestionDto> Data { get; set; }=new List<QuestionDto>();

        public long Total { get; set; }

        public QuestionListResult()
        {

        }

        public QuestionListResult(List<QuestionDto> data, long total)
        {
            Data = data;
            Total = total;
        }

        public class QuestionDto
        {
            public int Id { get; set; }
            /// <summary>
            /// 问题
            /// </summary>
            public string Problem { get; set; }

            /// <summary>
            /// 问题类型
            /// </summary>
            public QuestionType Type { get; set; }

            /// <summary>
            /// 展示图片
            /// </summary>
            public string Img { get; set; }

            /// <summary>
            /// 答案解析
            /// </summary>
            public string AnswerAnalysis { get; set; }

            /// <summary>
            /// 问题选项
            /// </summary>
            public List<QuestionOptionsDetailResult> Options { get; set; }


        }
    }
}
