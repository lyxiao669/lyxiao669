using MediatR;
using System.Collections.Generic;

namespace AdminApi.Application
{
    public class CreateQuestionBankCommand:IRequest<bool>
    {

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get;  set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get;  set; }

        /// <summary>
        /// 时间限制
        /// </summary>
        public int TimeLimit { get;  set; }

        /// <summary>
        /// 题目数量
        /// </summary>
        public int Amount { get;  set; }
    }

}
