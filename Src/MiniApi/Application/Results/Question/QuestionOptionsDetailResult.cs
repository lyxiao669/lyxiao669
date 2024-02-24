using System.Collections.Generic;

namespace MiniApi.Application
{
    public class QuestionOptionsDetailResult
    {
            /// <summary>
            /// 选项Id
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 选项
            /// </summary>
            public string Option { get; set; }

            /// <summary>
            /// 选项答案内容
            /// </summary>
            public string OptionsAnswer { get; set; }

            /// <summary>
            /// 是否正确答案
            /// </summary>
            public bool IsAnswer { get; set; }
       
    }
}
