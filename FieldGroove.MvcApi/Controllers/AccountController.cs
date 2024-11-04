using Microsoft.AspNetCore.Mvc;
using FieldGroove.MvcApi.Data;
using FieldGroove.MvcApi.Models;
using Microsoft.EntityFrameworkCore;
using FieldGroove.MvcApi.Services;

namespace FieldGroove.MvcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext dbcontext;
        public AccountController(IConfiguration configuration, ApplicationDbContext dbcontext)
        {
            this.configuration = configuration;
            this.dbcontext = dbcontext;
        }

        // Login Action in Api Controller

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginModel entity)
        {
            if (ModelState.IsValid)
            {
                var isUser = await dbcontext.UserData.AsQueryable().AnyAsync(x => x.Email == entity.Email!);
                if (isUser)
                {
                  return Ok();
                }
            }
            return NotFound();
        }

        // Register Action in Api Controller

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterModel entity)
        {
            if (ModelState.IsValid)
            {
                var isUser = await dbcontext.UserData.AsQueryable().AnyAsync(x => x.Email == entity.Email!);
                if (!isUser)
                {
                    await dbcontext.UserData.AddAsync(entity);
                    await dbcontext.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest(new { error = "User already registered" });
            }
            return BadRequest(entity);
        }
    }
}
