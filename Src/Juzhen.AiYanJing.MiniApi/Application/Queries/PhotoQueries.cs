using Applet.API.Infrastructure;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class PhotoQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;
        readonly UserAccessor _userAccessor;
        readonly IPhotoService _photoService;

        public PhotoQueries(ApplicationDbContext context, UserAccessor userAccessor, IPhotoService photoService)
        {
            _context = context;
            _userAccessor = userAccessor;
            _photoService = photoService;
        }



        /// <summary>
        /// 照片墙
        /// </summary>
        /// <returns></returns>
        public  List<string> GetPhotoListResultAsync(int number)
        {
            var photoList=new List<string>();
            var user =  _context.IdentityUsers
                .Where(a=>a.IsPhoto==true)
                .OrderByDescending(a => a.UpdateTime)
                .Take(number)
                .ToList();
            foreach(var photo in user)
            {
                photoList.Add(photo.Photo);
            }

            return   photoList;
        }




        /// <summary>
        /// 自己的眼睛
        /// </summary>
        /// <returns></returns>
        public async Task<PhotoResult> GetPhotoResultAsync(string qrCode)
        {
            var user=await _context.IdentityUsers
                .Where(a=>a.QRCode==qrCode)
                .FirstOrDefaultAsync();

            var result = user.PhotoMap();

            return result;
        }
    }
}
