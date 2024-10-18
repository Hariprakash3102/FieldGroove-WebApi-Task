using FieldGroove.Api.Data;
using FieldGroove.Api.Model;
using Microsoft.AspNetCore.Mvc;
using FieldGroove.Api.Services;

namespace FieldGroove.Api.Controllers
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

		[HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginModel entity)
		{
            if (ModelState.IsValid)
            {
                var response = await dbcontext.UserData.FindAsync(entity.Email);
                if (response is not null)
                {
                    return Ok(response);
                }
            }
            return NotFound();
        }

		[HttpPost("Register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Register([FromBody] RegisterModel entity)
		{
			if (ModelState.IsValid)
			{
				await dbcontext.UserData.AddAsync(entity);
				return Ok(entity);

			}
			return BadRequest();
		}
	}
}
