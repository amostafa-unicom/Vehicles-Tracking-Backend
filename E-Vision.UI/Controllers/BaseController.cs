using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace E_Vision.UI.Controllers
{
    public class BaseController : ControllerBase
    {
        #region Properties
        public IConfiguration Configuration { get; set; }
        public int _userId
        {
            get
            {
                try
                {
                    if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.Sid), out int _currentUserId) && _currentUserId > default(int))
                        return _currentUserId;
                    else
                        throw new InvalidCredentialException("Invalid UserId");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string _culture
        {
            get
            {
                try
                {
                    var HeaderCulture = HttpContext.Request.Headers.Where(x => x.Key.Equals("Accept-Language"))?.FirstOrDefault();
                    if (HeaderCulture == null || !HeaderCulture.HasValue)
                        return Configuration.GetSection("DefaultLan").Value; 
                    else
                        return HeaderCulture.Value.Value[0].ToString();
                }
                catch (Exception ex)
                {
                    return Configuration.GetSection("DefaultLan").Value;
                }
            }
        }

        //public string _token
        //{
        //    get
        //    {
        //        try
        //        {
        //            var token = HttpContext.Request.Headers.Where(x => x.Key.Equals("Authorization"))?.FirstOrDefault();
        //            if (token == null || !token.HasValue)
        //                throw new BadRequestException();
        //            else
        //                return token.Value.Value[0].ToString();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        #endregion
    }
}
