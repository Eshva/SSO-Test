// Проект: Eshva.SsoTest.UI
// Имя файла: ShellViewModel.cs
// GUID файла: A2AEE239-0BF4-41BC-BB37-40D69610DB23
// Автор: Mike Eshva (mike@eshva.ru)
// Дата создания: 12.07.2017

#region Usings

using System.Reflection;
using System.Windows;
using Caliburn.Micro;

#endregion


namespace Eshva.SsoTest.UI.ViewModels
{
    public sealed class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel(IUriReceiver uriReceiver)
        {
            uriReceiver.RegisterUriHandler(HandleUri);

        }

        private void HandleUri(string uri)
        {
            // TODO: Handle authentication URI.
            MessageBox.Show(uri, "URI recieved");
        }

        public void InstallCustomUriScheme()
        {
            var installer = new CustomUriSchemeInstaller();
            var applicationPath = Assembly.GetExecutingAssembly().Location;
            installer.registerUriScheme(ApplicationCustomUriSchemeProtocol, "Eshva SSO Test application protocol", applicationPath);
            MessageBox.Show("Title", $"Custom URI scheme '{ApplicationCustomUriSchemeProtocol}' is installed.");
        }

        private readonly SsoAuthenticationProvider _authenticationProvider;
        private const string ApplicationCustomUriSchemeProtocol = "eveauth-sso-test";
    }
}