#region Usings

using System;
using System.IO;
using System.IO.Pipes;

#endregion


namespace Eshva.SsoTest.UI.Services
{
    public sealed class NamedPipesUriReceiver : IUriReceiver
    {
        public void RegisterUriHandler(Action<string> handleUri)
        {
            _handleUri = handleUri;
            StartListeningPipes();
        }

        private void StartListeningPipes()
        {
            _serverStream = new NamedPipeServerStream(
                Constants.PipeName,
                PipeDirection.InOut,
                1,
                PipeTransmissionMode.Message,
                PipeOptions.Asynchronous);
            _serverStream.BeginWaitForConnection(
                asyncResult =>
                {
                    _serverStream.EndWaitForConnection(asyncResult);
                    _serverStream.WaitForPipeDrain();
                    using (var reader = new StreamReader(_serverStream))
                    {
                        var message = reader.ReadToEnd();
                        _handleUri(message);
                    }

                    StartListeningPipes();
                },
                null);
        }

        private NamedPipeServerStream _serverStream;
        private Action<string> _handleUri;
    }
}