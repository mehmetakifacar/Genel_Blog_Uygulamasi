using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }


        public bool Authenticate(string roleName, HttpContext httpContext)
        {
            string userName = httpContext.User.Identity.Name;
            var user = _adminDal.Get(c => c.UserName == userName);
            if (user != null && user.Role == roleName)
            {
                return true;
            }
            return false;

        }


        public ClaimsPrincipal LogIn(Admin admin)
        {
            if (admin != null)
            {
                var adminValues = _adminDal.List().FirstOrDefault
                    (x => x.UserName == admin.UserName && x.Password == admin.Password);

                if (adminValues != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,adminValues.UserName),
                        new Claim(ClaimTypes.Role,adminValues.Role)
                    };

                    var userIdentity = new ClaimsIdentity(claims, "AdminScheme");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    return principal;
                }
            }

            return null;
        }


        public async Task LogOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync("AdminScheme");
        }



    }
}






