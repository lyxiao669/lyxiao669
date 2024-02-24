using System;
using System.Collections.Generic;

namespace MiniApi.Application
{
    public class RecordList
    {
        public int Number { get; set; }

        public List<AnswerResultRecordResultList.AnswerResultRecordDto> Record { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Mark { get; set; }

        /// <summary>
        /// 答题时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
