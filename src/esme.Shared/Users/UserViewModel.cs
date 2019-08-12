using System;
using System.Collections.Generic;

namespace esme.Shared.Users
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
