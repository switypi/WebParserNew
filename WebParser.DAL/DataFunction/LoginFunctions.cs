using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.DAL.Model;

namespace WebParser.DAL.DataFunction
{
    public class LoginFunctions
    {
        /// <summary>
        /// Login to web parser
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginDTO DoLogin(string userId, string password)
        {
            LoginDTO itemDTO = new LoginDTO();
            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                var userProfileObj = context.UserProfiles.FirstOrDefault(item => item.UserId == userId && item.Password == password);
                if (userProfileObj != null)
                {
                    itemDTO.IsValidLogin = true;
                    itemDTO.IsAdmin = userProfileObj.Admin;
                }
            }
            return itemDTO;
        }
    }
}
