#region Usings

using System;

#endregion


namespace Eshva.SsoTest.UI.Services
{
    public interface IUriReceiver
    {
        void RegisterUriHandler(Action<string> handleUri);
    }
}