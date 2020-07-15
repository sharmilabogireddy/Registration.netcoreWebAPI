using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClaimSearchWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace ClaimSearchWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private UserRegistrationContext dbContext = new UserRegistrationContext();

        [Route("[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetStudents()
        {
            try
            {
                return await dbContext.Users.ToListAsync();

            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        //POST: /Registration
        public async Task<ActionResult> PostUser(Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var isExist = IsEmailExist(user.EmailId);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return BadRequest();
                }
                user.UserId = Guid.NewGuid();
                //user.Password = Crypto.Hash(user.Password);
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
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
