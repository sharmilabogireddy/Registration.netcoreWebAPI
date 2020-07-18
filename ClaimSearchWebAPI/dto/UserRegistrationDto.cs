using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClaimSearchWebAPI.dto
{
    public partial class UserRegistrationDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
