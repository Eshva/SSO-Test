#region Usings

using System.IO;
using System.IO.Pipes;

#endregion


namespace Eshva.SsoTest.UI.Services
{
    internal sealed class NamedPipesUriSender
    {
        public void SendUri(string uri)
        {
            using (var namedPipeClient = new NamedPipeClientStream(Constants.PipeName))
            {
                namedPipeClient.Connect();
                using (var writer = new StreamWriter(namedPipeClient))
                {
                    writer.AutoFlush = true;
                    writer.Write(uri);
                }
            }
        }
    }
}