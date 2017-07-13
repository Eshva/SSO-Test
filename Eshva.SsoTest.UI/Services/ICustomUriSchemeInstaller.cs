namespace Eshva.SsoTest.UI.Services
{
	public interface ICustomUriSchemeInstaller
	{
		void InstallUriScheme(
			string protocol,
			string displayName,
			string applicationPath,
			string command = "open",
			int iconIndex = 1);
	}
}