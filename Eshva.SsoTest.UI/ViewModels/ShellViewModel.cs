// Проект: Eshva.SsoTest.UI
// Имя файла: ShellViewModel.cs
// GUID файла: A2AEE239-0BF4-41BC-BB37-40D69610DB23
// Автор: Mike Eshva (mike@eshva.ru)
// Дата создания: 12.07.2017

#region Usings

using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Eshva.SsoTest.UI.Services;

#endregion


namespace Eshva.SsoTest.UI.ViewModels
{
	public sealed class ShellViewModel : PropertyChangedBase
	{
		public ShellViewModel(IUriReceiver uriReceiver, ICustomUriSchemeInstaller uriSchemeInstaller, IUserNotifier userNotifier)
		{
			uriReceiver.RegisterUriHandler(HandleUri);
			_uriSchemeInstaller = uriSchemeInstaller;
			_userNotifier = userNotifier;
		}

		public void InstallCustomUriScheme()
		{
			_uriSchemeInstaller.InstallUriScheme(
				ApplicationCustomUriSchemeProtocol,
				"Eshva SSO Test application protocol",
				Assembly.GetExecutingAssembly().Location);
			_userNotifier.InfomUser($"Custom URI scheme '{ApplicationCustomUriSchemeProtocol}' is installed.");
		}

		private void HandleUri(string uri)
		{
			// TODO: Handle authentication URI.
			MessageBox.Show(uri, "URI received");
		}

		private readonly ICustomUriSchemeInstaller _uriSchemeInstaller;
		private readonly IUserNotifier _userNotifier;
		private readonly SsoAuthenticationProvider _authenticationProvider;
		private const string ApplicationCustomUriSchemeProtocol = "eveauth-sso-test";
	}
}