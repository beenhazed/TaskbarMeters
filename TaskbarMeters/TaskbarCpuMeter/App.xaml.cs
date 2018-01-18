using System;
using System.Diagnostics;
using System.Windows;
using TaskbarCore;

namespace TaskbarCpuMeter
{
	public partial class App : Application
	{
		private readonly PerformanceCounter _counter = new PerformanceCounter();

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			_counter.CategoryName = "Processor";
			_counter.CounterName = "% Processor Time";
			_counter.InstanceName = "_Total";

			var mainWindow = new MainWindow();
			mainWindow.Tick += WhenTimerTick;
			mainWindow.Show();
		}

		private void WhenTimerTick(object sender, EventArgs e)
		{
			((MainWindow)sender).SetTaskBarStatus((int)_counter.NextValue());
		}
	}
}
