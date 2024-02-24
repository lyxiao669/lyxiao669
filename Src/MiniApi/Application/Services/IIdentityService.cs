using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public interface IIdentityService
    {
        int Id { get; }
    }

    public class IdentityService : IIdentityService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
       
        public IdentityService(IHttpContextAccessor httContextAccessor)
        {
            _httpContextAccessor = httContextAccessor;
        }
    
        public int Id
        {
            get 
            {
                var nameIdentifier = _httpContextAccessor.HttpContext.User.FindFirst(a => a.Type == ClaimTypes.NameIdentifier).Value;
                return Convert.ToInt32(nameIdentifier);
            }
        }
    }
}
