﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string role);
    }
}
