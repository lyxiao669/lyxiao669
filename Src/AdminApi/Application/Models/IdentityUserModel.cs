namespace AdminApi.Application
{
    public class IdentityUserModel:PageModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get;  set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get;  set; }

        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get; set; }
    }
}
