
using Applet.API.Infrastructure;
using Domain.Aggregates;
using Domain.Aggregates.QuestionOptionsAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class ExamAnswerCommandHandler : IRequestHandler<ExamAnswerCommand, ExamResult>
    {
        readonly UserAccessor _userAccessor;
        readonly IIdentityUserRepository _identityUserRepository;
        readonly IAnswerResultRecordRepository _answerResultRecordRepository;
        readonly IQuestionRepository _questionRepository;
        readonly IQuestionOptionsRepository _questionOptionsRepository;
        readonly IQuestionBankRepository _questionBankRepository;
        readonly IAchievementRepository _achievementRepository;

        public ExamAnswerCommandHandler(UserAccessor userAccessor, IAnswerResultRecordRepository answerResultRecordRepository, IQuestionRepository questionRepository = null, IQuestionOptionsRepository questionOptionsRepository = null, IAchievementRepository achievementRepository = null, IQuestionBankRepository questionBankRepository = null, IIdentityUserRepository identityUserRepository = null)
        {
            _userAccessor = userAccessor;
            _answerResultRecordRepository = answerResultRecordRepository;
            _questionRepository = questionRepository;
            _questionOptionsRepository = questionOptionsRepository;
            _achievementRepository = achievementRepository;
            _questionBankRepository = questionBankRepository;
            _identityUserRepository = identityUserRepository;
        }

        public async Task<ExamResult> Handle(ExamAnswerCommand request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.Id;

            var user = await _identityUserRepository.GetAsync(userId);

            var bank = await _questionBankRepository.GetByGradeAsync(user.Grade);

            var answerNumber = await _answerResultRecordRepository.GetNumberByUserId(userId);

            var TotalScore = 0.00;

            var sum = bank.Amount;
            double SingleScore = 100 / sum;
            var rightQuestion = 0;
            var errorQuestion = 0;

            for(var x=request.AnswerList.Count-1; x>=0; x--)
            {
                var item=request.AnswerList[x];

                var anserResult = new AnswerResultRecord(userId, item.QuestionId, item.QuestionOption);

                anserResult.AddNumber(answerNumber + 1);

                _answerResultRecordRepository.Add(anserResult);

                var rightList = await _questionOptionsRepository.GetByQuestionIdRightListAsync(item.QuestionId);

                #region 单选题
                if (rightList.Count == 1)
                {
                    if (item.QuestionOption == rightList[0].Option)
                    {
                        TotalScore += SingleScore;
                        rightQuestion += 1;
                        anserResult.Result(true);
                    }
                    else
                    {
                        errorQuestion += 1;
                    }
                }
                #endregion
                else
                {
                    var flag = false;
                    var userAnswer = item.QuestionOption.Split(",");

                    //答案选项
                    var anserStringList = string.Empty;
                    for (int i = 0; i < rightList.Count; i++)
                    {
                        anserStringList += rightList[i].Option;

                    }
                    if (userAnswer.Length != anserStringList.Length)
                    {
                        errorQuestion += 1;
                        continue;
                    }
                    //拿到用户的回答选项
                    foreach (var userItem in userAnswer)
                    {
                        if (anserStringList.Contains(userItem))
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }

                    if (flag == false)
                    {
                        errorQuestion += 1;
                    }
                    else
                    {
                        rightQuestion += 1;
                        TotalScore += SingleScore;
                        anserResult.Result(true);
                    }

                }
            }

            var result = $"共{sum}题，答对{rightQuestion}题，答错{errorQuestion}题";

            var achievement = new Achievement(
                userId: userId,
                mark: (int)TotalScore,
                result: result,
                number:answerNumber+1
                );
            _achievementRepository.Add(achievement);

            await _achievementRepository.UnitOfWork.SaveChangeAsync(cancellationToken);

            var examResult = new ExamResult
            {
                Mark=achievement.Mark,
                Result=achievement.Result,
            };

            return examResult;

        }
    }
}
