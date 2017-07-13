#region Usings

using System;
using System.Text;

#endregion


namespace Eshva.SsoTest.UI
{
	internal static class Constants
	{
		public static string UserAgent = $"{ApplicationUriScheme}-application";
		public static string AuthorizationHttpHeaderValue =
			$"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ApplicationClientId}:{ApplicationSecretKey}"))}";

		public static readonly string CallbackUrl = $"{ApplicationUriScheme}://auth";
		public static readonly string PipeName = $@"\\.\pipe\{ApplicationUriScheme}";
		public const string ApplicationUriScheme = "eveauth-sso-test";
		public const string ApplicationClientId = "f4d794d5f0da4aa6bea297d2dfef5a56";
		public const string ApplicationSecretKey = "D3aBYA6IQW879FbLVDmAEHd9WoB7lLcxNvhrx2nb";
	}
}