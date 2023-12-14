using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace HR.LeaveManagement.BlazorUI.Services
{
	public class AuthenticationService : BaseHttpService, IAuthenticationService
	{
		private readonly AuthenticationStateProvider _authenticationStateProvider;		

		public AuthenticationService(IClient client, ILocalStorageService localStorage,
			AuthenticationStateProvider authenticationStateProvider) :base(client, localStorage)
		{
			_authenticationStateProvider = authenticationStateProvider;
		}

		public async Task<bool> AuthenticateAsync(string email, string password)
		{
			try
			{
				AuthRequest authenticationRequest = new AuthRequest()
				{
					Email = email,
					Password = password
				};
				var authenticationRespnse = await _client.LoginAsync(authenticationRequest);

				if (authenticationRespnse.Token != string.Empty)
				{
					await _localStorage.SetItemAsync("token", authenticationRespnse.Token);
					//set claims in Blazon and login state
					await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				return false;
			}
			

		}

		public async Task Logout()
		{
			//remove claims in Blazor and invalidate login state
			await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
		}

		public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string userName, string password)
		{
			RegistrationRequest registrationRequest = new RegistrationRequest()
			{
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				UserName = userName,
				Password = password
			};
			var response = await _client.RegisterAsync(registrationRequest);

			if (string.IsNullOrEmpty(response.UserId) == false)
			{
				return true;
			}
			return false;
		}
	}
}
