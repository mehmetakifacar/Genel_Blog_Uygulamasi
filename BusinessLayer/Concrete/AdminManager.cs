using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
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
            if (httpContext.User.Identity.IsAuthenticated)
            {
                string username = httpContext.User.Identity.Name;
                var user = _adminDal.Get(c => c.UserName == username);

                if (user != null && user.Role == roleName)
                {
                    return true; // Kullanıcı belirtilen role sahip
                }
            }
            return false; // Kullanıcı belirtilen role sahip değil
        }

        public ClaimsPrincipal Login(Admin admin)
        {
            if (admin != null)
            {
                var adminValues = _adminDal.List().FirstOrDefault(
                    x => x.UserName == admin.UserName && x.Password == admin.Password);

                if (adminValues != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, adminValues.UserName),
                    new Claim(ClaimTypes.Role, adminValues.Role)
                };

                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    return principal;
                }
            }

            return null;
        }

        //public bool Authenticate(string roleName, HttpContext httpContext)
        //{
        //    string username = httpContext.User.Identity.Name;
        //    var user = _adminDal.Get(c => c.UserName == username);
        //    if (user != null && user.Role == roleName)
        //    {
        //        return true; // Kullanıcı belirtilen role sahip
        //    }
        //    return false; // Kullanıcı belirtilen role sahip değil
        //}

        //public ClaimsPrincipal Login(Admin admin)
        //{
        //    if (admin != null)
        //    {
        //        var adminValues = _adminDal.List().Where(
        //        x => x.UserName == admin.UserName && x.Password == admin.Password).FirstOrDefault();

        //        if (adminValues != null)
        //        {
        //            var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, adminValues.UserName),
        //            new Claim(ClaimTypes.Role, adminValues.Role)
        //        };

        //            var userIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
        //            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

        //            return principal;
        //        }
        //    }

        //    return new ClaimsPrincipal();
    }
}

