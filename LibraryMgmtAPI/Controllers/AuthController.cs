using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryMgmtAPI.Model;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LibraryMgmtDbContext _context;

        public AuthController(LibraryMgmtDbContext context)
        {
            _context = context;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Member>> Login([FromBody] LoginRequest loginRequest)
        {
            var member = await _context.Members
                .Where(m => m.Username == loginRequest.Username && m.Password == loginRequest.Password)
                .FirstOrDefaultAsync();

            if (member == null)
            {
                return Unauthorized(); // Return 401 if login fails
            }

            return Ok(member); // Return 200 with member details if successful
        }
    }
}
