using MediatR;

namespace AdminApi.Application
{
    public class UpdateUserVsionCommand : IRequest<bool>
    {
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 左眼视力
        /// </summary>
        public string LeftEyeVision { get; set; }

        /// <summary>
        /// 右眼视力
        /// </summary>
        public string RightEyeVision { get; set; }

        /// <summary>
        /// 左眼散光
        /// </summary>
        public string LeftEyeAstigmatism { get; set; }

        /// <summary>
        /// 右眼散光
        /// </summary>
        public string RightEyeAstigmatism { get; set; }

        /// <summary>
        /// 左眼瞳距
        /// </summary>
        public string LeftEyePupilDistance { get; set; }
        /// <summary>
        /// 右眼瞳距
        /// </summary>
        public string RightEyePupilDistance { get; set; }

        /// <summary>
        /// 左眼轴向
        /// </summary>
        public string LeftEyeAxial { get; set; }

        /// <summary>
        /// 右眼轴向
        /// </summary>
        public string RightEyeAxial { get; set; }


        /// <summary>
        /// 医生建议
        /// </summary>
        public string DoctorAdvice { get; set; }
    }
}
