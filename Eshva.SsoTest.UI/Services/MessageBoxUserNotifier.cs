// Проект: Eshva.SsoTest.UI
// Имя файла: MessageBoxUserNotifier.cs
// GUID файла: A1E13182-1D57-427A-8E1D-D43A688FC41B
// Автор: Mike Eshva (mike@eshva.ru)
// Дата создания: 13.07.2017

#region Usings

using System.Windows;

#endregion


namespace Eshva.SsoTest.UI.Services
{
	public class MessageBoxUserNotifier : IUserNotifier
	{
		public void InfomUser(string message)
		{
			MessageBox.Show(message);
		}
	}
}