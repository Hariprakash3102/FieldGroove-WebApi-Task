﻿using FieldGroove.Api.Data;
using FieldGroove.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace FieldGroove.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly ApplicationDbContext dbcontext;
		public HomeController(IConfiguration configuration, ApplicationDbContext dbcontext)
		{
			this.configuration = configuration;
			this.dbcontext = dbcontext;
		}

		[HttpGet("Leads")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Leads()
		{
			var User = await dbcontext.Leads.ToListAsync();
			return Ok(User);
		}

		[HttpGet("Leads/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Leads(int id)
		{
			var User = await dbcontext.Leads.FindAsync(id);
			return Ok(User);
		}

		[HttpPost("CreateLead")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateLead([FromBody] LeadsModel model)
		{
			if (ModelState.IsValid)
			{
				await dbcontext.Leads.AddAsync(model);
				await dbcontext.SaveChangesAsync();
				return Ok();
			}
			return BadRequest();
		}


		[HttpPut("EditLead")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> EditLead([FromBody] LeadsModel model)
		{
			if (ModelState.IsValid)
			{
				if (await dbcontext.Leads.AnyAsync(x => x.Id == model.Id))
				{
					dbcontext.Leads.Update(model);
					await dbcontext.SaveChangesAsync();
					return Ok(model);
				}
				return NotFound();
			}
			return BadRequest();
		}

		[HttpDelete("Delete/{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var response = await dbcontext.Leads.FindAsync(id);
			if (response is not null)
			{
				dbcontext.Leads.Remove(response);
				await dbcontext.SaveChangesAsync();
				return Ok(response);
			}
			return NotFound();
		}

		[HttpGet("download-csv")]
		public async Task<IActionResult> DownloadCsv()
		{
			var records = await dbcontext.Leads.ToListAsync();

			var csv = new StringBuilder();
			csv.AppendLine("ID,Project Name,Status,Added,Type,Contact,Action,Assignee,Bid Date");

			foreach (var record in records)
			{
				csv.AppendLine($"{record.Id},{record.ProjectName},{record.Status},{record.Added},{record.Type},{record.Contact},{record.Action},{record.Assignee},{record.BidDate}");
			}

			byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
			return File(buffer, "text/csv", "LeadsData.csv");
		}

	}
}
