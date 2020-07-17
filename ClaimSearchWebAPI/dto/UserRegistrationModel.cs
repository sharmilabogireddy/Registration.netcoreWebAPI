using System;
using System.Collections.Generic;

namespace ClaimSearchWebAPI.dto
{
    public partial class UserRegistrationModel
    {
        public string userName { get; set; }
        public string emailId { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
    }
}
