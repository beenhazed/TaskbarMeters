using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Taskbar;
using Application=System.Windows.Application;
using MessageBox=System.Windows.MessageBox;
using PowerLineStatus=System.Windows.Forms.PowerLineStatus;

namespace TaskbarBatteryMeter
{
	public partial class App : Application
	{
		private MainWindow _mainWindow;
		private PowerLineStatus _powerLineStatus;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			_mainWindow = new MainWindow();
			_mainWindow.Show();

			_powerLineStatus = SystemInformation.PowerStatus.PowerLineStatus;

			var timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += WhenTimerTick;
			timer.Start();

			TaskbarManager.Instance.ApplicationId = "Taskbar Battery Meter";
		}

		private void WhenTimerTick(object sender, EventArgs e)
		{
			var powerStatus = SystemInformation.PowerStatus;

			//if (powerStatus.PowerLineStatus != _powerLineStatus)
			//{
			//    _powerLineStatus = powerStatus.PowerLineStatus;

			//    Uri iconUri;

			//    if (_powerLineStatus == PowerLineStatus.Online)
			//    {
			//        iconUri = new Uri("pack://application:,,,/Images/Battery.ico", UriKind.RelativeOrAbsolute);
			//    }
			//    else
			//    {
			//        iconUri = new Uri("pack://application:,,,/Images/PluggedIn.ico", UriKind.RelativeOrAbsolute);
			//    }

			//    _mainWindow.Icon = BitmapFrame.Create(iconUri);
			//}

			var percentRemaining = (int)(powerStatus.BatteryLifePercent * 100);
			var state = TaskbarProgressBarState.Normal;

			if (percentRemaining < 20)
			{
				state = percentRemaining < 10 ? TaskbarProgressBarState.Error : TaskbarProgressBarState.Paused;
			}

			TaskbarManager.Instance.SetProgressState(state);
			TaskbarManager.Instance.SetProgressValue(percentRemaining, 100);
		}
	}
}
