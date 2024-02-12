namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class UserInformationResult
    {
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get;  set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get;  set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get;  set; }

        /// <summary>
        /// 是否使用机器看过
        /// </summary>
        public bool IsSee { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get;  set; }

        /// <summary>
        /// 合成照片
        /// </summary>
        public string CompositePhoto { get;  set; }
    }
}
