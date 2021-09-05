using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]  // Client needs to specify api/Users/<method name>
    public class UsersController : BaseApiController // ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        // To get all Users
        // [HttpGet]
        // public ActionResult<IEnumerable<AppUser>> GetUsers()
        // {
        //     // Synchronous Code. This locks the table until data is fetched. 
        //     // Not best practice as another user has to wait. Blocks thread
        //     return _context.Users.ToList();   
        // }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            // Asynchronous Code. Better Approach
            return await _context.Users.ToListAsync();   
        }

        // api/user/3
        // To get particular User
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}