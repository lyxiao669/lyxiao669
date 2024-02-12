using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class StudentUserQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public StudentUserQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 学生详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IdentityUser> GetIdentityUserDetailAsync(int id)
        {
            var result = await _context.IdentityUsers
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }


        /// <summary>
        /// 学生列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageResult<IdentityUser>> GetIdentityUserListAsync(IdentityUserModel model)
        {
            var query=_context.IdentityUsers
                .WhereIF(a=>a.FullName.Contains(model.FullName),model.FullName!=null)
                .WhereIF(a=>a.Mobile.Contains(model.Mobile),model.Mobile!=null)
                .WhereIF(a => a.School.Contains(model.School), model.School != null)
                .WhereIF(a => a.Grade.Contains(model.Grade), model.Grade != null);

            var list = await query
                .OrderByDescending(a => a.CreateTime)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return PageResult(list, count);
        }

        /// <summary>
        /// 学生年级列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetStudentGradeListAsync()
        {
            var userList =await _context.IdentityUsers
                .ToListAsync();

            var grade = new List<string>();

            for (int i = 0; i < userList.Count; i++)
            {
                var item=userList[i];

                if (i == 0)
                {
                    grade.Add(item.Grade);
                    continue;
                }

                var flag = 0;
                foreach(var gradeItem in grade)
                {
                    if (gradeItem == item.Grade)
                    {
                        flag ++;
                        continue;
                    }
                }
                if(flag == 0)
                {
                    grade.Add(item.Grade);
                }
            }
            return grade;
        }


    }
}
