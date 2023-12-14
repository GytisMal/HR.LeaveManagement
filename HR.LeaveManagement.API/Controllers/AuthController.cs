﻿using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authenticationService;

		public AuthController(IAuthService authenticationService)
        {
			this._authenticationService = authenticationService;
		}

		[HttpPost("Login")]
		public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
		{
			return Ok(await _authenticationService.Login(request));
		}

		[HttpPost("Register")]
		public async Task<ActionResult<RegistrationResponse>> Registration(RegistrationRequest request)
		{
			return Ok(await _authenticationService.Register(request));
		}
	}
}