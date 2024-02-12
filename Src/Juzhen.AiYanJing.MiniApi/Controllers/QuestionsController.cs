using Juzhen.AiYanJing.MiniApi.Application;
using Juzhen.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        readonly QuestionQueries _queries;
        readonly IMediator _mediator;

        public QuestionsController(QuestionQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        /// 登录进入答题
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(QuestionListResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var data = await _queries.GetQuestionListAsync();
            return Ok(data);
        }

        /// <summary>
        /// 答题时间
        /// </summary>
        /// <returns></returns>
        [HttpGet("Time")]
        [Authorize]
        [ProducesResponseType(typeof(QuestionBankDetailResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTime()
        {
            var data = await _queries.GetTimeAsync();
            return Ok(data);
        }

        ///// <summary>
        ///// 扫码后进入答题
        ///// </summary>
        ///// <param name="id">用户Id</param>
        ///// <returns></returns>
        //[HttpGet("{id:int}")]
        //[ProducesResponseType(typeof(QuestionListResult), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult> Get(int id)
        //{
        //    var data = await _queries.GetQuestionListByQrCodeAsync(id);
        //    return Ok(data);
        //}

        /// <summary>
        /// 小程序用户查看答题记录列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("Record")]
        [Authorize]
        [ProducesResponseType(typeof(PageResult<RecordList>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetRecordResultListAsync([FromQuery] QuestionModel model)
        {
            var data = await _queries.GetRecordResultListAsync(model);
            return Ok(data);
        }

        /// <summary>
        /// 答题列表的问题记录详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("RecordDetail")]
        [Authorize]
        [ProducesResponseType(typeof(QuestionListResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetQuestionOptionResultAsync(int questionId)
        {
            var data = await _queries.GetQuestionOptionResultAsync(questionId);
            return Ok(data);
        }




        /// <summary>
        /// 用户答题
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ExamResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post(ExamAnswerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
