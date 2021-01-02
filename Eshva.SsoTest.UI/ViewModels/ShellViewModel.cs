// Проект: Eshva.SsoTest.UI
// Имя файла: ShellViewModel.cs
// GUID файла: A2AEE239-0BF4-41BC-BB37-40D69610DB23
// Автор: Mike Eshva (mike@eshva.ru)
// Дата создания: 12.07.2017

#region Usings

using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Caliburn.Micro;
using Eshva.SsoTest.UI.Services;
using Newtonsoft.Json;

#endregion


namespace Eshva.SsoTest.UI.ViewModels
{
	public sealed class ShellViewModel : PropertyChangedBase
	{
		public ShellViewModel(
			IUriReceiver uriReceiver,
			ICustomUriSchemeInstaller uriSchemeInstaller,
			ISsoAuthenticationService authenticationService)
		{
			uriReceiver.RegisterUriHandler(HandleUri);
			_uriSchemeInstaller = uriSchemeInstaller;
			_authenticationService = authenticationService;

			Log("HOW TO USE");
			Log("For the first time press the topmost button to register a custom URI-scheme.");
			Log("This will produce a .reg file in the temp-folder which will be imported");
			Log("into your registry. It's safe. You can check the contents of this file afterwards.");
			Log("If you want to check it beforehand look at the source code at:");
			Log("https://github.com/Eshva/SSO-Test");
			Log("");
			Log("After that you can check the next two buttons to authenticate WITH and WITHOUT");
			Log("the client secret key by pressing the second and the third buttons.");
			Log("");
			Log("The button #4 is to check API call of your character standings. (not implemented yet)");
		}

		// ReSharper disable once UnusedMember.Global
		public string Output => _output.ToString();

		// ReSharper disable once UnusedMember.Global
		public void InstallCustomUriScheme()
		{
			Log("");
			Log($"Installing a custom URI scheme {Constants.ApplicationUriScheme}...");

			_uriSchemeInstaller.InstallUriScheme(
				Constants.ApplicationUriScheme,
				"Eshva SSO Test application protocol",
				Assembly.GetExecutingAssembly().Location);

			Log($"Custom URI scheme '{Constants.ApplicationUriScheme}' is installed.");
			Log("Afterwards you can remove this URI scheme by deleting the registry key:");
			Log($@"HKEY_CLASSES_ROOT\{Constants.ApplicationUriScheme}");
		}

		// ReSharper disable once UnusedMember.Global
		public void LoginUsingSsoWithClientSecret()
		{
			Log("");
			Log("Try to get access and refresh tokens using the OAuth 2 Authentication flow");
			Log("WITH sending the client secret key.");
			Log("Expected to get them. But this way we DISCLOSE the secret key through source code");
			Log("but we should not do it according to security reasons!");
			Log("");
			Log("Open authentication page in default browser...");
			_authenticationService.AuthenticateWithClientSecret(doUseApplicationSecretKey : true);
		}

		// ReSharper disable once UnusedMember.Global
		public void LoginUsingSsoWithoutClientSecret()
		{
			Log("");
			Log("Try to get access and refresh tokens using the OAuth 2 Authentication flow");
			Log("WITHOUT sending the client secret key.");
			Log("Expecting to get them accourding to the new recomendation.");
			Log("But CCP SSO don't authenticate us.");
			Log("");
			Log("Open authentication page in default browser...");
			_authenticationService.AuthenticateWithClientSecret(doUseApplicationSecretKey : false);
		}

		public void GetStandingsForCharacter()
		{
			
		}

		private void HandleUri(string uri)
		{
			try
			{
				Log("Authentication code from SSO received:");
				Log(uri);
				Log("Requesting access tokens...");

				_tokens = _authenticationService.GetTokens(uri);
				Log("Tokens received:");
				Log($"{JsonConvert.SerializeObject(_tokens)}");
			}
			catch (WebException exception)
			{
				Log(exception.ToString());

				var responseStream = exception.Response.GetResponseStream();
				if (responseStream != null)
				{
					using (var reader = new StreamReader(responseStream))
					{
						Log(reader.ReadToEnd());
						Log("CCP PLEASE FIX this error. You should comply the current recomendations.");
						Log("Otherwise you LIMIT us to server based web-applications or the implicit flow");
						Log("which requires our applicaiton users to authenticate desktop, SPA and mobile");
						Log("applications each 20 minutes. This is not acceptable!!!");
					}
				}
			}
		}

		private void Log(string message)
		{
			_output.AppendLine(message);
			NotifyOfPropertyChange(nameof(Output));
		}

		private TokensResponse _tokens;

		private readonly StringBuilder _output = new StringBuilder();
		private readonly ICustomUriSchemeInstaller _uriSchemeInstaller;
		private readonly ISsoAuthenticationService _authenticationService;
	}
}