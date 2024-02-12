using Juzhen.Domain.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace AdminApi.Application
{
    public class CreateQuestionCommand:IRequest<bool>
    {
        /// <summary>
        /// 问题
        /// </summary>
        public string Problem { get;  set; }


        /// <summary>
        /// 问题类型
        /// </summary>
        public QuestionType Type { get;  set; }

        /// <summary>
        /// 展示图片
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 答案解析
        /// </summary>
        public string AnswerAnalysis { get; set; }

    }

}
