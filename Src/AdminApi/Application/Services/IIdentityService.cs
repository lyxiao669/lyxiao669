using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public interface IIdentityService
    {
        int Id { get; }
    }

    public class IdentityService : IIdentityService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
       
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    
        public int Id
        {
            get
            {
                var identfier = _httpContextAccessor.HttpContext.User.FindFirst(t => t.Type == ClaimTypes.NameIdentifier).Value;
                return Convert.ToInt32(identfier);
            }
        }
    }
}
