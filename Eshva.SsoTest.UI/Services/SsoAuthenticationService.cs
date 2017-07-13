#region Usings

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

#endregion


namespace Eshva.SsoTest.UI.Services
{
	internal sealed class SsoAuthenticationService : ISsoAuthenticationService
	{
		public void AuthenticateWithClientSecret(bool doUseApplicationSecretKey)
		{
			_doUseApplicationSecretKey = doUseApplicationSecretKey;
			var requestString = CreateCodeRequestString(new[] { "esi-characters.read_standings.v1" }, State);
			Process.Start(requestString);
		}

		public TokensResponse GetTokens(string uri)
		{
			var ssoAuthenticationCodeResponse = new Uri(uri);
			var queryParameters = HttpUtility.ParseQueryString(ssoAuthenticationCodeResponse.Query);
			var responseState = queryParameters.Get("state");
			if (!responseState.Equals(State))
			{
				throw new Exception($"State value is wrong. Received: '{responseState}' but should be: '{State}'.");
			}

			var authorizationCode = queryParameters.Get("code");
			return RequestTokens(authorizationCode);
		}

		private TokensResponse RequestTokens(string authorizationCode)
		{
			var request = WebRequest.CreateHttp("https://login.eveonline.com/oauth/token");
			request.Method = WebRequestMethods.Http.Post;
			request.UserAgent = Constants.UserAgent;
			request.ContentType = "application/x-www-form-urlencoded";
			request.Host = "login.eveonline.com";

			if (_doUseApplicationSecretKey)
			{
				request.Headers.Add(HttpRequestHeader.Authorization, Constants.AuthorizationHttpHeaderValue);
			}

			var content = new ASCIIEncoding().GetBytes($"grant_type=authorization_code&code={authorizationCode}&client_id={Constants.ApplicationClientId}");
			var requestStream = request.GetRequestStream();
			requestStream.Write(content, 0, content.Length);

			var response = request.GetResponse();
			var data = string.Empty;
			var responseStream = response.GetResponseStream();
			if (responseStream != null)
			{
				using (var reader = new StreamReader(responseStream))
				{
					data = reader.ReadToEnd();
				}
			}

			return JsonConvert.DeserializeObject<TokensResponse>(data);
		}

		private string CreateCodeRequestString(string[] scopes, string state) => new StringBuilder()
			.Append("https://login.eveonline.com/oauth/authorize/?")
			.Append("response_type=code")
			.Append($"&redirect_uri={HttpUtility.UrlEncode(Constants.CallbackUrl)}")
			.Append($"&client_id={Constants.ApplicationClientId}")
			.Append($"&scope={string.Join(" ", scopes)}")
			.Append($"&state={state}").ToString();

		private bool _doUseApplicationSecretKey;

		private const string State = "test-state";
	}
}