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

		//[HttpPost]
		//public async Task<IActionResult> Login([FromBody] LoginModel entity)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var response = await dbcontext.UserData.FindAsync(entity.Email);
		//		if (response is not null)
		//		{
		//			return Ok(response);
		//		}
		//	}
		//	return NotFound();
		//}

		[HttpPost]
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

		//[HttpPost]
		//public async Task<IActionResult> ForgotPassword(string email)
		//{
		//	try
		//	{
		//		string password = await unitOfWork.UserRepository.IsValidEmail(email);
		//		string subject = "Feild Groove reset password";
		//		string messageBody = "Your Field groove Password is " + password;
		//		emailSender.EmailSendAsync(email, subject, messageBody);
		//	}
		//	catch (Exception ex)
		//	{
		//		ViewBag.ErrorMessage = ex.Message;
		//		return View();
		//	}
		//	return RedirectToAction("ChangePassword");
		//}
		//[HttpGet]
		//public IActionResult ChangePassword()
		//{
		//	return View();
		//}
	}
}
