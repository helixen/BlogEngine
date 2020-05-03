using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers
{
    public static class BlogHelper
    {
        public const string SessionKeyUserName = "_UserName";
        public const string SessionKeyUserId = "_UserId";
        public const string SessionKeyUserRole = "_UserRole";
    }
}
