using Juzhen.Domain.Event;
using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("identity_user")]
    public class IdentityUser:AggregateRoot
    {
        /// <summary>
        /// 并发控制
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; private set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get;private set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get;private set; }


        /// <summary>
        /// 学校
        /// </summary>
        public string School { get;private set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get;private set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get;private set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get;private set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string QRCode { get; private set; }

        /// <summary>
        /// 二维码图片
        /// </summary>
        public string QrCodeImg { get; private set; }

        /// <summary>
        /// 是否生成二维码图片
        /// </summary>
        public bool IsQrCode { get; private set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get;private set; }

        /// <summary>
        /// 合成照片
        /// </summary>
        public string CompositePhoto { get;private set; }

        /// <summary>
        /// 是否拍过
        /// </summary>
        public bool IsPhoto { get; private set; }= false;


        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; private set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get;private set; }=DateTime.Now;

        public IdentityUser()
        {
            
        }

        /// <summary>
        /// 导入学生数据
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="mobile"></param>
        /// <param name="school"></param>
        /// <param name="grade"></param>
        /// <param name="age"></param>
        /// <param name="gender"></param>

        public IdentityUser(string fullName, string mobile, string school, string grade, string age, string gender):this()
        {
            FullName = fullName;
            Mobile = mobile;
            School = school;
            Grade = grade;
            Age = age;
            Gender = gender;
        }

        public void Update(string fullName, string mobile, string school, string grade, string age, string gender)
        {
            FullName = fullName;
            Mobile = mobile;
            School = school;
            Grade = grade;
            Age = age;
            Gender = gender;
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="qRCode"></param>

        public void Update(string qRCode)
        {
            QRCode = qRCode;
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="qrCodeImg"></param>
        public void AddQrCodeImg(string qrCodeImg)
        {
            IsQrCode = true;
            QrCodeImg = qrCodeImg;
        }

        /// <summary>
        /// 拍照片
        /// </summary>
        /// <param name="photo"></param>

        public void GeneratePhotos(string photo)
        {
            Photo = photo;
            IsPhoto = true;

            UpdateTime=DateTime.Now;

            AddDominEvent(new AddPhotoQueueDomainEvent(this));

            //AddDominEvent(new PhotoDomainEvent(this));
        }

        /// <summary>
        /// 合成照片
        /// </summary>
        /// <param name="compositePhoto"></param>
        public void CompositePhotos(string compositePhoto)
        {
            CompositePhoto = compositePhoto;
        }


    }
}
