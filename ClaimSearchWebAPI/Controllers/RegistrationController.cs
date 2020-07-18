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
using ClaimSearchWebAPI.Helper;
using ClaimSearchWebAPI.Services;

namespace ClaimSearchWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private IUserService _userService;

        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = RoleDto.Admin)]
        [HttpPost]
        //POST: /Registration
        public IActionResult PostUser([FromBody] UserRegistrationDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var isExist = _userService.IsEmailExist(userDto.EmailId);
                if (isExist)
                {
                    return BadRequest(new { message = "User email is already exists." });
                }
                _userService.RegisterUser(userDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok();
        }

    }
}
