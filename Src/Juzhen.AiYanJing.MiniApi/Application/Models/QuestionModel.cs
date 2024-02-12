namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class QuestionModel:PageModel
    {
        /// <summary>
        /// false=最新,true=全部
        /// </summary>
        public bool ? IsAll { get; set; }=false;

        public int ? Number { get; set; }
    }
}
