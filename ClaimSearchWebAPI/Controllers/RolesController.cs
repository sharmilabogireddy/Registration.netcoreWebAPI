using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimSearchWebAPI.Models;

namespace ClaimSearchWebAPI.Controllers
{
    //[Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private UserRegistrationContext _context = new UserRegistrationContext();


        // GET: api/Roles
        [Route("[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Roles/5
        [Route("[controller]/{id}")]
        [HttpGet]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
            var roles = await _context.Roles.FindAsync(id);

            if (roles == null)
            {
                return NotFound(new { message = "RoleId Not Found" });
            }

            return roles;
        }
       
        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
    }
}
