using System;
using System.Windows;
using Microsoft.VisualBasic.Devices;
using TaskbarCore;
using TaskbarMemoryMeter.Properties;

namespace TaskbarMemoryMeter
{
	public partial class App : Application
	{
		private ComputerInfo	_computerInfo;
		private ulong			_totalPhysicalMemory;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			_computerInfo = new ComputerInfo();
			_totalPhysicalMemory = _computerInfo.TotalPhysicalMemory;

			var mainWindow = new MainWindow();
			mainWindow.Tick += WhenTimerTick;
			mainWindow.Show();
		}

		private void WhenTimerTick(object sender, EventArgs e)
		{
			var available = (double)(_totalPhysicalMemory-_computerInfo.AvailablePhysicalMemory) / _totalPhysicalMemory;
			((MainWindow)sender).SetTaskBarStatus((int)(available * 100));
		}
	}
}