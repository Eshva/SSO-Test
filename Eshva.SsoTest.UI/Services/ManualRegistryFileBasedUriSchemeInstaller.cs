#region Usings

using System.Diagnostics;
using System.IO;
using System.Text;

#endregion


namespace Eshva.SsoTest.UI.Services
{
	public sealed class ManualRegistryFileBasedUriSchemeInstaller : ICustomUriSchemeInstaller
	{
		public void InstallUriScheme(
			string protocol,
			string displayName,
			string applicationPath,
			string command = "open",
			int iconIndex = 1)
		{
			var registryFileContents = CreateRegistryFileContents(protocol, displayName, applicationPath, command, iconIndex);
			var registryFilePath = Path.Combine(Path.GetTempPath(), "custom-scheme-file.reg");
			File.WriteAllText(registryFilePath, registryFileContents, Encoding.ASCII);
			Process.Start("regedit.exe", $"/s \"{registryFilePath}\"");
		}

		private string CreateRegistryFileContents(string protocol, string displayName, string applicationPath, string command, int iconIndex)
		{
			var formattedApplicationPath = applicationPath.Replace("\\", "\\\\");
			return new StringBuilder()
				.AppendLine("Windows Registry Editor Version 5.00").AppendLine()
				.AppendLine($@"[HKEY_CLASSES_ROOT\{protocol}]")
				.AppendLine($"@=\"URL:{displayName}\"")
				.AppendLine("\"URL Protocol\"=\"\"").AppendLine()
				.AppendLine($@"[HKEY_CLASSES_ROOT\{protocol}\DefaultIcon]")
				.AppendLine($"@=\"{formattedApplicationPath},{iconIndex}\"").AppendLine()
				.AppendLine($@"[HKEY_CLASSES_ROOT\{protocol}\shell]").AppendLine()
				.AppendLine($@"[HKEY_CLASSES_ROOT\{protocol}\shell\{command}]").AppendLine()
				.AppendLine($@"[HKEY_CLASSES_ROOT\{protocol}\shell\open\command]")
				.AppendLine($"@=\"\\\"{formattedApplicationPath}\\\" \\\"%1\\\"\"").AppendLine().ToString();
		}
	}
}