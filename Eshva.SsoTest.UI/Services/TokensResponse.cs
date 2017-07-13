// Проект: Eshva.SsoTest.UI
// Имя файла: TokensResponse.cs
// GUID файла: 8FF83779-2D51-459E-92A3-2222C8D2BCAE
// Автор: Mike Eshva (mike@eshva.ru)
// Дата создания: 13.07.2017

using Newtonsoft.Json;


namespace Eshva.SsoTest.UI.Services
{
	public sealed class TokensResponse
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("expires_in")]
		public int LifeTimeInSeconds { get; set; }
	}
}