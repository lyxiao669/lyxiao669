namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class UserinfoResult
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
        /// 学校
        /// </summary>
        public string School { get;  set; }
        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get;  set; }

        /// <summary>
        /// 二维码图片
        /// </summary>
        public string QrCodeImg { get;  set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get;  set; }

        /// <summary>
        /// 合成照片
        /// </summary>
        public string CompositePhoto { get;  set; }

        /// <summary>
        /// 是否拍过照片
        /// </summary>
        public bool IsPhoto { get;  set; }
    }
}
