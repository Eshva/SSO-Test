// Проект: Eshva.SsoTest.UI
// Имя файла: AppBootstrapper.cs
// GUID файла: DC736190-4A91-4F39-B706-512144E24535
// Автор: Mike Eshva (mike@eshva.ru)
// Дата создания: 12.07.2017

#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Autofac;
using Caliburn.Micro.Autofac;
using Eshva.SsoTest.UI.Services;
using Eshva.SsoTest.UI.ViewModels;

#endregion


namespace Eshva.SsoTest.UI
{
	public sealed class AppBootstrapper : AutofacBootstrapper<ShellViewModel>
	{
		public AppBootstrapper()
		{
			Initialize();
		}

		protected override void OnStartup(object sender, StartupEventArgs eventArgs)
		{
			if (DoesApplicationUiInstanceStarted())
			{
				SendUriToAnotherApplicationInstance(eventArgs.Args);
			}
			else
			{
				if (!eventArgs.Args.Any())
				{
					DisplayRootViewFor<ShellViewModel>();
				}
				else
				{
					Application.Current.Shutdown(0);
				}
			}
		}

		protected override void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterType<NamedPipesUriReceiver>().As<IUriReceiver>();
			builder.RegisterType<ManualRegistryFileBasedUriSchemeInstaller>().As<ICustomUriSchemeInstaller>();
			builder.RegisterType<SsoAuthenticationService>().As<ISsoAuthenticationService>();
		}

		private static bool DoesApplicationUiInstanceStarted()
		{
			var thisProcess = Process.GetCurrentProcess();
			return Process.GetProcesses()
						.Any(process => process.ProcessName.Equals(thisProcess.ProcessName) && process.Id != thisProcess.Id);
		}

		private void SendUriToAnotherApplicationInstance(IReadOnlyCollection<string> arguments)
		{
			if (arguments.Count != 1)
			{
				throw new ApplicationException("Application accepts only one parameter the URI string from redirection.");
			}

			var uriSender = new NamedPipesUriSender();
			uriSender.SendUri(arguments.Single());
			Application.Current.Shutdown(0);
		}
	}
}