using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    /// <summary>
    /// 轮播图
    /// </summary>
    [Table("Users")]
    public class Users:AggregateRoot
    {
        /// <summary>
        /// 并发控制
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get;  set; }

        //public int Id { get;  set; }
        public string UserName { get;  set; }
        public string Password { get;  set; }
        public string Avatar { get;  set; }

        public Users() { }

        public Users(string userName, string password, string avatar) : this()
        {
            UserName = userName;
            Password = password; 
            Avatar = avatar;
        }
        public void Update(string userName, string password, string avatar)
        {
            UserName = userName;
            Password = password;
            Avatar = avatar;
        }
    }
}
