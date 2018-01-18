using System;
using System.Diagnostics;
using System.Windows;
using TaskbarCore;
using TaskbarDiskIOMeter.Properties;

namespace TaskbarDiskIOMeter
{
	public partial class App : Application
	{
        private readonly PerformanceCounter _counter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

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
