using System;
using System.Collections.Generic;

namespace ClaimSearchWebAPI.Models
{
    public partial class Users
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string IsActive { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Roles Role { get; set; }
    }
}
