using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.DAL.DataModel;
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
                var userProfileObj = context.UserProfiles.FirstOrDefault(item => item.UserId == userId );
                if (userProfileObj != null)
                {
                    itemDTO.IsValidLogin = true;
                    itemDTO.IsAdmin = userProfileObj.Admin;
                    itemDTO.UserId = userProfileObj.UserId;
                }
                else
                {
                    return null;
                }

            }
            return itemDTO;
        }

        public LoginDTO DoRegister(LoginDTO item)
        {
            LoginDTO dto = new LoginDTO();
            UserProfile profile = new UserProfile();
            profile.Admin = item.IsAdmin;
            profile.Password = item.Password;
            profile.UserId = item.UserId;

            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                bool isUserIDExist = context.UserProfiles.Any(c => c.UserId == profile.UserId);
                if (isUserIDExist)
                    return null;
                else
                {
                    context.UserProfiles.Add(profile);
                    int value = context.SaveChanges();
                    dto = context.UserProfiles.Where(c => c.UserId == profile.UserId && c.Password == profile.Password).Select(v => new LoginDTO()
                        {
                            IsAdmin = v.Admin,
                            UserId = v.UserId,
                        }).FirstOrDefault();
                }
            }
            return dto;
        }
    }
}
