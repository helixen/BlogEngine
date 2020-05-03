using System;

namespace BlogEngineAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Writer = 0,
        Editor = 1
    }
}
