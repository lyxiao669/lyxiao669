using System;
using System.Collections.Generic;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class AnswerResultRecordResultList
    {

        public List<AnswerResultRecordDto> Data { get; set; }=new List<AnswerResultRecordDto>();

        public AnswerResultRecordResultList()
        {

        }

        public AnswerResultRecordResultList(List<AnswerResultRecordDto> data)
        {
            Data = data;
        }

        public class AnswerResultRecordDto
        {
            /// <summary>
            /// 问题Id
            /// </summary>
            public int QuestionId { get; set; }

            /// <summary>
            /// 选择问题选项
            /// </summary>
            public string QuestionOption { get; set; }

            /// <summary>
            /// 是否答对
            /// </summary>
            public bool IsTrue { get; set; } = false;


            /// <summary>
            /// 答题时间
            /// </summary>
            public DateTime CreateTime { get; set; } = DateTime.Now;

            /// <summary>
            /// 第几次答题
            /// </summary>
            public int Number { get; set; } = 0;
        }

    }
}
