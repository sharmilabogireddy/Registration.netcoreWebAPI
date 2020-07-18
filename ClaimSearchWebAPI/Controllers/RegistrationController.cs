using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClaimSearchWebAPI.Models;
using ClaimSearchWebAPI.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;

namespace ClaimSearchWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private UserRegistrationContext dbContext = new UserRegistrationContext();

        [AllowAnonymous]
        [HttpPost]      
        //POST: /Registration
        public IActionResult PostUser([FromBody]UserRegistrationModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var isExist = IsEmailExist(user.emailId);
                if (isExist)
                {
                    return BadRequest(new { message = "User email is already exists." });
                }
                //user.password = Crypto.Hash(user.password);
               // dbContext.Users.Add(user);
               // dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error"); 
            }
            return Ok(user);
        }
        private bool IsEmailExist(string emailID)
        {
            using (UserRegistrationContext dc = new UserRegistrationContext())
            {
                var v = dc.Users.Where(a => a.EmailId == emailID).FirstOrDefault();
                return v != null;
            }
        }



    }
}
