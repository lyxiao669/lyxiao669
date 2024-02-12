namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class QuestionOptionResult
    {
        /// <summary>
        /// 问题Id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 问题标题
        /// </summary>
        public string Problem { get; set; }


        /// <summary>
        /// 正确选项
        /// </summary>
        public string ResultOption { get; set; }
    }
}
