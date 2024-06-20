using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JecubNode;

namespace Jecub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // POST: api/Users/register 
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            _context.Users.Add(new User() { Login = user.Login, Password = user.Password });
            await _context.SaveChangesAsync();
            await Console.Out.WriteLineAsync(string.Join(" ", _context.Users.ToList()));
            var userFound = _context.Users.ToList().Find(e => e.Login == user.Login && e.Password == user.Password);
            if (userFound == null)
            {
                return NotFound();
            }

            return Ok(userFound.Id);
        }

        // POST: api/Users/login 
        [HttpPost("/login")]
        public async Task<ActionResult<int>> Login([FromBody] User user)
        {

            var userFound = _context.Users.ToList().Find(e => e.Login == user.Login && e.Password == user.Password);
            if (userFound == null)
            {
                return NotFound();
            }

            return Ok(userFound.Id);

        }

    }
}
