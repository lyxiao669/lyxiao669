using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    /// <summary>
    /// 用户视力
    /// </summary>
    [Table("user_vsion")]
    public class UserVsion:AggregateRoot
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get;private set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get;private set; }

        /// <summary>
        /// 左眼视力
        /// </summary>
        public string LeftEyeVision { get;private set; }

        /// <summary>
        /// 右眼视力
        /// </summary>
        public string RightEyeVision { get;private set; }

        /// <summary>
        /// 左眼散光
        /// </summary>
        public string LeftEyeAstigmatism { get;private set; }

        /// <summary>
        /// 右眼散光
        /// </summary>
        public string RightEyeAstigmatism { get;private set; }

        /// <summary>
        /// 左眼瞳距
        /// </summary>
        public string LeftEyePupilDistance { get;private set; }
        /// <summary>
        /// 右眼瞳距
        /// </summary>
        public string RightEyePupilDistance { get; private set; }

        /// <summary>
        /// 左眼轴向
        /// </summary>
        public string LeftEyeAxial { get;private set; }

        /// <summary>
        /// 右眼轴向
        /// </summary>
        public string RightEyeAxial { get;private set; }


        /// <summary>
        /// 医生建议
        /// </summary>
        public string DoctorAdvice { get;private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get;private set; }=DateTime.Now;

        public UserVsion()
        {

        }


        public UserVsion(string fullName, string mobile, string leftEyeVision, string rightEyeVision, string leftEyeAstigmatism, string rightEyeAstigmatism, string leftEyePupilDistance, string rightEyePupilDistance, string leftEyeAxial, string rightEyeAxial, string doctorAdvice)
        {
            FullName = fullName;
            Mobile = mobile;
            LeftEyeVision = leftEyeVision;
            RightEyeVision = rightEyeVision;
            LeftEyeAstigmatism = leftEyeAstigmatism;
            RightEyeAstigmatism = rightEyeAstigmatism;
            LeftEyePupilDistance = leftEyePupilDistance;
            RightEyePupilDistance = rightEyePupilDistance;
            LeftEyeAxial = leftEyeAxial;
            RightEyeAxial = rightEyeAxial;
            DoctorAdvice = doctorAdvice;
        }

        public void Update(string fullName, string mobile, string leftEyeVision, string rightEyeVision, string leftEyeAstigmatism, string rightEyeAstigmatism, string leftEyePupilDistance, string rightEyePupilDistance, string leftEyeAxial, string rightEyeAxial, string doctorAdvice)
        {
            FullName = fullName;
            Mobile = mobile;
            LeftEyeVision = leftEyeVision;
            RightEyeVision = rightEyeVision;
            LeftEyeAstigmatism = leftEyeAstigmatism;
            RightEyeAstigmatism = rightEyeAstigmatism;
            LeftEyePupilDistance = leftEyePupilDistance;
            RightEyePupilDistance = rightEyePupilDistance;
            LeftEyeAxial = leftEyeAxial;
            RightEyeAxial = rightEyeAxial;
            DoctorAdvice = doctorAdvice;
        }
    }
}
