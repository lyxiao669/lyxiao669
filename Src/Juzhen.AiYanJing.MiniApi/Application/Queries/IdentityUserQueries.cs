using Applet.API.Infrastructure;
using Juzhen.Infrastructure;
using Juzhen.MiniProgramAPI;
using Juzhen.MiniProgramAPI.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class IdentityUserQueries : BaseQueries
    {
        readonly UserAccessor _userAccessor;
        readonly ApplicationDbContext _context;
        readonly JwtSecurityTokenService _tokenService;

        public IdentityUserQueries(
            UserAccessor userAccessor,
            ApplicationDbContext context, JwtSecurityTokenService tokenService)
        {
            _userAccessor = userAccessor;
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AccessTokenResult> Login(IdentiyUserModel model)
        {
            var user = await _context.IdentityUsers.Where(a => a.FullName == model.FullName && a.Mobile == model.Mobile)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                throw new ServiceException("没有该用户信息");
            }

            var cliams = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.Mobile.ToString())
            };
            var token = _tokenService.CreateToken(cliams);
            var data = new AccessTokenResult
            {
                AccessToken = token,
            };

            return data;

        }


        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        public async Task<UserinfoResult> GetUserInfo()
        {
            var user = await _context.IdentityUsers.FindAsync(_userAccessor.Id);

            return new UserinfoResult
            {
                Id = user.Id,
                FullName = user.FullName,
                Mobile = user.Mobile,
                Grade = user.Grade,
                Photo = user.Photo,
                CompositePhoto = user.CompositePhoto,
                IsPhoto = user.IsPhoto,
                School = user.School,
                QrCodeImg= user.QrCodeImg
            };
        }


        /// <summary>
        /// 二维码获取用户Token
        /// </summary>
        /// <param name="qRCode">二维码</param>
        /// <returns></returns>
        public async Task<AccessTokenResult> GetQrcodeUserInfo(string qRCode)
        {
            var user = await _context.IdentityUsers.Where(a => a.QRCode == qRCode)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            var cliams = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.Mobile.ToString())
            };
            var token = _tokenService.CreateToken(cliams);
            var data = new AccessTokenResult
            {
                AccessToken = token,
            };

            return data;
        }


        /// <summary>
        /// 二维码获取用户信息
        /// </summary>
        /// <param name="qRCode">二维码</param>
        /// <returns></returns>
        public async Task<UserInformationResult> GetUserInfo(string qRCode)
        {
            var user = await _context.IdentityUsers.Where(a=>a.QRCode==qRCode)
                .FirstOrDefaultAsync();

            var result= user.Map();

            return result;
        }

    }
}
