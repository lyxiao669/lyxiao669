
using AdminApi.Application;
using Juzhen.IdentityUI.Domain;
using Juzhen.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUI.API.Controllers
{
    /// <summary>
    /// 后台用户
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class IdentityUserController : ControllerBase
    {
        #region 公共服务
        readonly ILogger<IdentityUserController> _logger;
        readonly DefaultIdentityUIDbContext _context;
        readonly IJwtSecurityTokenService _jwtSecurityTokenService;
        readonly IIdentityService _identityService;
        public IdentityUserController(
            ILogger<IdentityUserController> logger,
            DefaultIdentityUIDbContext context,
            IIdentityService identityService,
            IJwtSecurityTokenService jwtSecurityTokenService)
        {
            _logger = logger;
            _context = context;
            _jwtSecurityTokenService = jwtSecurityTokenService;
            _identityService = identityService;
        }
        #endregion

        [HttpGet("Grant/Menus")]
        public async Task<List<IdentityMenu>> GetGrantMenusList()
        {
            var roleMenus = await _context.Users
                .Include(user => user.Role)
                .ThenInclude(role => role.Menus)
                .Where(a => a.Id == _identityService.Id)
                .Select(a => a.Role)
                .SelectMany(a => a.Menus)
                .Select(a => a.MenuId)
                .ToListAsync();
            return await _context.Menus
                .Where(a => roleMenus.Contains(a.Id))
                .ToListAsync();
        }

        [HttpGet]
        public async Task<List<__IdentityUser>> GetList([FromQuery] GetUsersModel model)
        {
            var role = await _context.Roles.FindAsync(model.RoleId);

            return await _context.Users.Where(a => a.Role == role).Include(a => a.Role).ToListAsync();
        }

        /// <summary>
        /// 修改用户基本信息，权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("Roles")]
        public async Task UpdateRole(UpdateUserRoleModel model)
        {
            var user = await _context.Users.FindAsync(model.Id);
            var role = await _context.Roles.FindAsync(model.RoleId);
            user.Role = role;
            user.HeadImg = model.HeadImg;
            user.Name = model.Name;
            user.Mobile = model.Mobile;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateUser(CreateUserModel model)
        {
            #region 非空验证
            if (string.IsNullOrEmpty(model.Account))
            {
                throw new ServiceException("账号不能为空");
            }
            if (string.IsNullOrEmpty(model.PassWord))
            {
                throw new ServiceException("密码不能为空");
            }
            #endregion


            if (await _context.Users.Where(a => a.Account == model.Account).AnyAsync())
            {
                throw new ServiceException("该账号已存在");
            }

            var role = await _context.Roles.FindAsync(model.RoleId);

            model.PassWord = Md5_32BitHash(model.PassWord);

            await _context.Users.AddAsync(new __IdentityUser()
            {
                HeadImg = model.HeadImg,
                Mobile = model.Mobile,
                Name = model.Name,
                Account = model.Account,
                Password = model.PassWord,
                Role = role
            });

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<TokenResponse> Login(LoginModel model)
        {
            #region 非空验证
            if (string.IsNullOrEmpty(model.Account))
            {
                throw new ServiceException("账号不能为空");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ServiceException("密码不能为空");
            }
            #endregion

            var password = Md5_32BitHash(model.Password);

            var user = await _context.Users
                .Where(a => a.Account == model.Account)
                .Where(a => a.Password == password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ServiceException("用户名或密码错误");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            return _jwtSecurityTokenService.CreateToken(claims, 3600 * 24 * 3);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserInfo")]
        public async Task<UserInfoResults> UserInfo()
        {
            var user = await _context.Users.FindAsync(_identityService.Id);

            UserInfoResults results = new UserInfoResults()
            {
                Account = user.Account,
                HeadImg = user.HeadImg,
                Id = user.Id,
                Mobile = user.Mobile,
                Name = user.Name,
                Role = user.Role
            };


            return results;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("PassWord")]
        public async Task UpdatePassWord(UpdatePassWordModel model)
        {
            var user = await _context.Users.FindAsync(model.Id);

            user.Password = Md5_32BitHash(model.PassWord);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public static string Md5_32BitHash(string text, bool lower = true)
        {
            using var cryp = MD5.Create();
            var data = cryp.ComputeHash(Encoding.UTF8.GetBytes(text));
            var crypdata = string.Join("", data.Select(s => s.ToString(lower ? "x2" : "X2")));
            return crypdata;
        }
    }


    #region Results

    public class UserInfoResults
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string HeadImg { get; set; }

        public string Mobile { get; set; }

        public string Account { get; set; }

        public IdentityRole Role { get; set; }
    }

    public class LoginResults
    {
        public string Token { get; set; }
    }

    #endregion

    #region Model

    public class GetUsersModel
    {
        public int RoleId { get; set; }
    }

    public class UpdatePassWordModel
    {
        public int Id { get; set; }

        public string PassWord { get; set; }
    }

    public class LoginModel
    {
        public string Account { get; set; }

        public string Password { get; set; }
    }

    public class CreateUserModel
    {
        public string Name { get; set; }

        public string HeadImg { get; set; }

        public string Mobile { get; set; }
        public string Account { get; set; }

        public string PassWord { get; set; }
        public int RoleId { get; set; }
    }
    public class UpdateUserRoleModel
    {
        public string Name { get; set; }

        public string HeadImg { get; set; }

        public string Mobile { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }
    }

    #endregion
}




