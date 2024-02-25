using Applet.API.Infrastructure;
using Domain.Aggregates;
using Infrastructure;
using Juzhen.MiniProgramAPI;
using Juzhen.MiniProgramAPI.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniApi.Application
{
  public class UsersQueries : BaseQueries
  {
    readonly UsersAccessor _usersAccessor;
    readonly ApplicationDbContext _context;
    readonly JwtSecurityTokenService _tokenService;

    public UsersQueries(ApplicationDbContext context, UsersAccessor usersAccessor, JwtSecurityTokenService tokenService)
    {
      _context = context;
      _usersAccessor = usersAccessor;
      _tokenService = tokenService;
    }

    public async Task<bool> RegisterUser(UsersModel model)
    {
      var existingUser = await _context.Users
          .AnyAsync(u => u.UserName == model.UserName);
      if (existingUser)
      {
        throw new ServiceException("用户已存在");
      }

      var user = new Users
      {
        UserName = model.UserName,
        Password = model.Password, // 注意：实际应用中应该对密码进行加密处理
        Avatar = model.Avatar
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return true;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<AccessTokenResult> Login(UsersLoginModel model)
    {
      var user = await _context.Users.Where(a => a.UserName == model.UserName && a.Password == model.Password)
          .FirstOrDefaultAsync();

      if (user == null)
      {
        throw new ServiceException("没有该用户信息");
      }

      var cliams = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.Password.ToString())
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
    /// <exception cref="ServiceException"></exception>
    public async Task<UsersInfoResult> GetUserDetails()
        {
            var user = await _context.Users.FindAsync(_usersAccessor.Id);

            if (user == null)
            {
                throw new ServiceException("用户不存在");
            }

            return new UsersInfoResult
            {
                Id = user.Id,
                UserName = user.UserName,
                Avatar = user.Avatar,
                // 可以添加更多需要返回的用户信息
            };
        }
  }
}
