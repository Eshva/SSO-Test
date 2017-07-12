namespace Eshva.SsoTest.UI
{
	internal class SsoAuthenticationProvider
	{
		public SsoAuthenticationProvider(string uriSchemeProtocol)
		{
			_uriSchemeProtocol = uriSchemeProtocol;
		}

		public void AuthenticateWithClientSecret()
		{
		}

		private readonly string _uriSchemeProtocol;
	}
}