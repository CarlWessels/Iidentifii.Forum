using Iidentifii.Forum.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library
{
    public interface IUserManager
    {
        User? Login(string email, string password);
        int Create(User user, string password);

        void ResetPassword();
    }
}
