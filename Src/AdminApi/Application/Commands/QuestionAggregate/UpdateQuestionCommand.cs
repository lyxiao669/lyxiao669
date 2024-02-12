using Juzhen.Domain.Aggregates;
using MediatR;

namespace AdminApi.Application
{
    public class UpdateQuestionCommand:IRequest<bool>
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
        public string Img { get;  set; }

        /// <summary>
        /// 答案解析
        /// </summary>
        public string AnswerAnalysis { get;  set; }
    }
}
