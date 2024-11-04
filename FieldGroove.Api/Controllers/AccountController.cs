using FieldGroove.Api.Data;
using FieldGroove.Api.Interfaces;
using FieldGroove.Api.Models;
using FieldGroove.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FieldGroove.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly IUnitOfWork unitOfWork;
		public AccountController(IConfiguration configuration, IUnitOfWork unitOfWork)
		{
			this.configuration = configuration;
			this.unitOfWork = unitOfWork;
		}

		// Login Action in Api Controller

		[HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginModel entity)
		{
            if (ModelState.IsValid)
            {
				var isUser = await unitOfWork.UserRepository.IsValid(entity);
                if (isUser)
                {
					var JwtToken = new JwtToken(configuration);

					var response = new
					{
						User = entity.Email!,
						Token = JwtToken.GenerateJwtToken(entity.Email!),
                        Status = "OK",
						Timestamp = DateTime.Now
					};
                    return Ok(response);
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
				var isUser = await unitOfWork.UserRepository.IsRegistered(entity);
                if (!isUser)
				{
					await unitOfWork.UserRepository.Create(entity);
					return Ok();
				}
				return BadRequest(new { error = "User already registered" });
			}
			return BadRequest(entity);
		 }
	}
}
