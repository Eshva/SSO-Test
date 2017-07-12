#region Usings

using System;

#endregion


namespace Eshva.SsoTest.UI
{
    public interface IUriReceiver
    {
        void RegisterUriHandler(Action<string> handleUri);
    }
}