using MediatR;
using System.Collections.Generic;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class ExamAnswerCommand:IRequest<ExamResult>
    {     
        public List<AnswerResultRecordVo> AnswerList { get; set; }=new List<AnswerResultRecordVo>();
    }

    public class AnswerResultRecordVo
    {
        /// <summary>
        /// 问题Id
        /// </summary>
        public int QuestionId { get;  set; }


        /// <summary>
        /// 选择问题选项
        /// </summary>
        public string QuestionOption{ get;  set; }
    }
    public class ExamResult
    {
        /// <summary>
        /// 分数
        /// </summary>
        public int Mark { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
    }

}
