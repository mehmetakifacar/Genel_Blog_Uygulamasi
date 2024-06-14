using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WriterManager : IWriterService
    {
        IWriterDal _writerDal;
        MvcContext context = new MvcContext();
        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }
               

        public Writer GetById(int id)
        {
            return _writerDal.Get(x => x.WriterId == id);
        }

        public List<Writer> GetWriterList()
        {
            return _writerDal.List();
        }

        public ClaimsPrincipal LogIn(Writer writer)
        {
            if (writer != null)
            {
                var writerValues = _writerDal.List().FirstOrDefault
                    (x => x.Mail == writer.Mail && x.Password == writer.Password);

                if (writerValues != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email,writerValues.Mail),
                    };

                    var userIdentity = new ClaimsIdentity(claims, "WriterScheme");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    return principal;
                }
            }

            return null;
        }

        public async Task LogOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync("WriterScheme");
        }

        public void WriterAdd(Writer writer)
        {
            _writerDal.Insert(writer);
        }

        public void WriterDelete(Writer writer)
        {
            _writerDal.Delete(writer);
        }

        public void WriterUpdate(Writer writer)
        {
            _writerDal.Update(writer);
        }
    }
}
