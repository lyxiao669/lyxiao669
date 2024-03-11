using Applet.API.Infrastructure;
using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class QuestionQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;
        readonly UserAccessor _userAccessor;

        public QuestionQueries(ApplicationDbContext context, UserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// 回答时间
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionBankDetailResult> GetTimeAsync()
        {
            //用户
            var user = await _context.IdentityUsers
                .Where(a => a.Id == _userAccessor.Id)
                .FirstOrDefaultAsync();

            //用户题库
            var bank = await _context.QuestionBanks.Where(a => a.Grade == user.Grade)
                .FirstOrDefaultAsync();

            var result= bank.BankMap();
            return result;
        }

        /// <summary>
        /// 答题问题列表
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionListResult> GetQuestionListAsync()
        {

            //用户
            var user = await _context.IdentityUsers
                .Where(a => a.Id == _userAccessor.Id)
                .FirstOrDefaultAsync();

            //用户题库
            var bank = await _context.QuestionBanks.Where(a => a.Grade==user.Grade)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            //问题数量
            var amount=bank.Amount;

            #region 所有问题的Id
            var questionList =new List<int>();


            var query = _context.Questions;

            var list= await query
                .ToListAsync();

            foreach (var item in list)
            {
                questionList.Add(item.Id);
            }
            #endregion

            #region 随机问题题号
            var resultQuestionIdList = new List<int>();

            for (int j = 0; j < amount; j++)
            {
                Random rd =new Random();
                //下标
                int index=rd.Next(0, questionList.Count);


                if (j == 0)
                {
                    resultQuestionIdList.Add(questionList[index]);
                }
                else
                {
                    var flag = 0;

                    //判断是否有重复问题
                    foreach (var item in resultQuestionIdList)
                    {
                        if (questionList[index] == item)
                        {
                            flag++;
                            continue;
                        }
                    }

                    if (flag == 0)
                    {
                        resultQuestionIdList.Add(questionList[index]);
                    }
                    else
                    {
                        j--;
                    }
                }

            }

            var resultQuestionList = new List<Question>();
            
            foreach(var item in resultQuestionIdList)
            {
                var question = await _context.Questions
                    .Where(a => a.Id==item)
                    .FirstOrDefaultAsync();

                resultQuestionList.Add(question);
            }
            #endregion


            var resultList = resultQuestionList
                .OrderBy(a => a.Id)
                .ToList()
                .Map();

            foreach(var item in resultList.Data)
            {
                var options = await _context.QuestionOptions
                    .Where(a => a.QuestionId == item.Id)
                    .ToListAsync();
                var OptionList = new List<QuestionOptionsDetailResult>();

                foreach (var option in options)
                {
                    OptionList.Add(option.OptionsMap());
                }

                item.Options = OptionList;

            }

            resultList.Total= await query.CountAsync();

            return resultList;
        }
        #region 废弃
        /// <summary>
        /// 扫码小程序答题问题列表
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionListResult> GetQuestionListByQrCodeAsync(int userId)
        {
            //用户
            var user = await _context.IdentityUsers
                .Where(a => a.Id == userId)
                .FirstOrDefaultAsync();

            //用户题库
            var bank = await _context.QuestionBanks.Where(a => a.Grade == user.Grade)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            //问题数量
            var amount = bank.Amount;

            #region 所有问题的Id
            var questionList = new List<int>();


            var query = _context.Questions;

            var list = await query
                .ToListAsync();

            foreach (var item in list)
            {
                questionList.Add(item.Id);
            }
            #endregion

            #region 随机问题题号
            var resultQuestionIdList = new List<int>();

            for (int j = 0; j < amount; j++)
            {
                Random rd = new Random();
                //下标
                int index = rd.Next(0, questionList.Count);


                if (j == 0)
                {
                    resultQuestionIdList.Add(questionList[index]);
                }
                else
                {
                    var flag = 0;

                    //判断是否有重复问题
                    foreach (var item in resultQuestionIdList)
                    {
                        if (questionList[index] == item)
                        {
                            flag++;
                            continue;
                        }
                    }

                    if (flag == 0)
                    {
                        resultQuestionIdList.Add(questionList[index]);
                    }
                    else
                    {
                        j--;
                    }
                }

            }

            var resultQuestionList = new List<Question>();

            foreach (var item in resultQuestionIdList)
            {
                var question = await _context.Questions
                    .Where(a => a.Id == item)
                    .FirstOrDefaultAsync();

                resultQuestionList.Add(question);
            }
            #endregion


            var resultList = resultQuestionList
                .OrderBy(a => a.Id)
                .ToList()
                .Map();

            foreach (var item in resultList.Data)
            {
                var options = await _context.QuestionOptions
                    .Where(a => a.QuestionId == item.Id)
                    .ToListAsync();

                var OptionList = new List<QuestionOptionsDetailResult>();

                foreach (var option in options)
                {
                    OptionList.Add(option.OptionsMap());
                }
            }

            resultList.Total = await query.CountAsync();

            return resultList;
        }
        #endregion

        /// <summary>
        /// 用户答题记录列表
        /// </summary>
        /// <returns></returns>
        public async Task<PageResult<RecordList>> GetRecordResultListAsync(QuestionModel model)
        {
            var userId = _userAccessor.Id;

            var achievement = await _context.Achievements
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Number)
                .FirstOrDefaultAsync();

            var list = await _context.AnswerResultRecords
                .OrderByDescending(a=>a.CreateTime)
                .Where(a => a.UserId == userId)
                .ToListAsync();

            var result = list
                .ToList()
                .Map();

            #region 影响速度
            //foreach(var data in result.Data)
            //{
            //    var resultListOption = await _context.QuestionOptions
            //        .Where(a => a.QuestionId == data.QuestionId)
            //        .Where(a => a.IsAnswer == true)
            //        .ToListAsync();

            //    for (var i = 0;i< resultListOption.Count; i++)
            //    {
            //        var option = resultListOption[i];
            //        if (i != resultListOption.Count - 1)
            //        {
            //            data.ResultOption += option.Option + ",";
            //        }
            //        else
            //        {
            //            data.ResultOption += option.Option;
            //        }
            //    }
            //}
            #endregion

            var numberList =new List<int>();

            var recordList=new List<RecordList>();

            for (var i = 0; i < result.Data.Count; i++)
            {
                var item=result.Data[i];
                if (i == 0)
                {
                    numberList.Add(item.Number);
                }
                else
                {
                    var flag = 0;
                    foreach(var s in numberList)
                    {
                        if (item.Number == s)
                        {
                            flag++;
                        }
                    }

                    if(flag == 0)
                    {
                        numberList.Add(item.Number);
                    }
                }
            }

            foreach(var item in numberList)
            {
                var recordResult = result.Data.Where(a => a.Number == item).ToList();
                #region 

                //foreach(var recorditem in recordResult)
                //{
                //    var question = _context.Questions.Where(a => a.Id == recorditem.QuestionId).FirstOrDefault();

                //    if (question.Type == QuestionType.多选)
                //    {
                //        var resultAnswer = _context.QuestionOptions
                //            .Where(a => a.QuestionId == recorditem.QuestionId)
                //            .Where(a => a.IsAnswer == true)
                //            .ToList();
                //        var answerstring = string.Empty;
                //        for (var i = 0; i < resultAnswer.Count; i++)
                //        {
                //            if (i == resultAnswer.Count - 1)
                //            {
                //                answerstring = resultAnswer[i].Option;
                //                continue;
                //            }
                //            answerstring = resultAnswer[i].Option + ",";
                //        }

                //        var r=recorditem.QuestionOption.Split(",");

                //        var flag = false;
                //        for(var i = 0;i< r.Length; i++)
                //        {
                //            var resultString=r[i];
                //            if (answerstring.Contains(resultString))
                //            {
                //                flag = true;
                //                continue;
                //            }
                //            else
                //            {
                //                flag=false;
                //                break;
                //            }
                //        }

                //        if (flag)
                //        {
                //            recorditem.IsTrue = true;
                //        }
                //    }
                //    else
                //    {
                //        var resultAnswer = _context.QuestionOptions
                //            .Where(a => a.QuestionId == recorditem.QuestionId)
                //            .Where(a => a.IsAnswer == true)
                //            .FirstOrDefault();

                //        var answerstring = resultAnswer.Option;

                //        if (answerstring==recorditem.QuestionOption)
                //        {
                //            recorditem.IsTrue=true;
                //        }
                //    }

                //}
                #endregion  

                var mark = await _context.Achievements
                    .Where(a => a.UserId == userId)
                    .Where(a=>a.Number==item)
                    .FirstOrDefaultAsync();

                var record = new RecordList()
                {
                    Number=item,
                    Record = recordResult,
                    Mark = mark.Mark,
                    CreateTime = mark.CreateTime,
                    
                };

                recordList.Add(record);
            }

            var count=recordList.Count;
            if (model.IsAll == true)
            {
                recordList = recordList
                    .Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToList();

            }
            else if(model.IsAll == false &&model.Number==null)
            {
                recordList=recordList.Take(1).ToList();
            }else if (model.IsAll == false&&model.Number != null)
            {
                recordList = recordList.Where(a=>a.Number == model.Number).ToList();
            }
            return PageResult(recordList, count);
        }

        /// <summary>
        /// 题目记录详情
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task<QuestionListResult> GetQuestionOptionResultAsync(int questionId)
        {
            var list = await _context.Questions
                .Where(a => a.Id == questionId)
                .ToListAsync();

            var resultList = list.Map();

            foreach (var item in resultList.Data)
            {
                var options = await _context.QuestionOptions
                    .Where(a => a.QuestionId == item.Id)
                    .ToListAsync();

                var OptionList = new List<QuestionOptionsDetailResult>();

                foreach (var option in options)
                {
                    OptionList.Add(option.OptionsMap());
                }
                item.Options = OptionList;
            }


            return resultList;
        }
    }
}
