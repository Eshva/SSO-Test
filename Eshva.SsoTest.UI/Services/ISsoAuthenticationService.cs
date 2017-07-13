// Проект: Eshva.SsoTest.UI
// Имя файла: ISsoAuthenticationService.cs
// GUID файла: D0B62A37-3884-4DB8-9A78-A141C92C8A15
// Автор: Mike Eshva (mike@eshva.ru)
// Дата создания: 13.07.2017

namespace Eshva.SsoTest.UI.Services
{
	public interface ISsoAuthenticationService
	{
		void AuthenticateWithClientSecret(bool doUseApplicationSecretKey);

		TokensResponse GetTokens(string uri);
	}
}