using MediatR;

namespace AdminApi.Application
{
    public class CreateQuestionOptionsCommand:IRequest<bool>
    {
        /// <summary>
        /// 问题Id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        public string Option { get; set; }

        /// <summary>
        /// 选项答案内容
        /// </summary>
        public string OptionsAnswer { get; set; }

        /// <summary>
        /// 是否答案
        /// </summary>
        public bool IsAnswer { get; set; } = false;
    }
}
