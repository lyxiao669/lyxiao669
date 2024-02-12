using MediatR;

namespace AdminApi.Application
{
    public class CreateIdentityUserCommand:IRequest<bool>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }


        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
    }
}
