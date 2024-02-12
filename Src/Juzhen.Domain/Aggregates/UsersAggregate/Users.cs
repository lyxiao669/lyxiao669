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
    /// 轮播图
    /// </summary>
    [Table("Users")]
    public class Users:AggregateRoot
    {
        /// <summary>
        /// 并发控制
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; private set; }

        //public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Avatar { get; private set; }

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
