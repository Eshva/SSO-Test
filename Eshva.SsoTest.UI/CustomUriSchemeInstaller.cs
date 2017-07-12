#region Usings

using System;
using Microsoft.Win32;

#endregion


namespace Eshva.SsoTest.UI
{
	internal class CustomUriSchemeInstaller
	{
		public void registerUriScheme(
			string protocol,
			string displayName,
			string applicationPath,
			string command = "open",
			int iconIndex = 1)
		{
			try
			{
				var protocolRoot = CreateProtocolRoot(protocol, displayName, iconIndex);
				AddIcon(protocolRoot, applicationPath, iconIndex);
				AddCommand(protocolRoot, applicationPath, command);
			}
			catch (Exception exception)
			{
				// TODO: Specify concrete exception classes.
				throw;
			}
		}

		private RegistryKey CreateProtocolRoot(string protocol, string displayName, int iconIndex)
		{
			var protocolRoot = Registry.ClassesRoot.CreateSubKey(protocol);
			protocolRoot.SetValue(string.Empty, $"URL:{displayName}", RegistryValueKind.String);
			protocolRoot.SetValue("URL Protocol", string.Empty, RegistryValueKind.String);
			return protocolRoot;
		}

		private void AddIcon(RegistryKey protocolRoot, string applicationPath, int iconIndex)
		{
			var iconKey = protocolRoot.CreateSubKey("DefaultIcon");
			iconKey.SetValue(string.Empty, $"{applicationPath},{iconIndex}", RegistryValueKind.String);
		}

		private void AddCommand(RegistryKey protocolRoot, string applicationPath, string command)
		{
			var commandKey = protocolRoot.CreateSubKey("shell").CreateSubKey(command).CreateSubKey("command");
			commandKey.SetValue(string.Empty, $"\"{applicationPath}\" \"%1\"");
		}
	}
}