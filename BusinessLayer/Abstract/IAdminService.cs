using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        ClaimsPrincipal LogIn(Admin admin);
        Task LogOutAsync(HttpContext httpContext);
        bool Authenticate(string roleName, HttpContext httpContext);

    }
}
