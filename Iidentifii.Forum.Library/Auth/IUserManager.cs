using Iidentifii.Forum.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Auth
{
    public interface IUserManager
    {
        (int, string)? Login(string email, string password);
        int Create(string name, string email , string password);

        void ResetPassword();
        void CreateModerator(int userId);
    }
}
