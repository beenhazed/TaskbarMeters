using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TaskbarBatteryMeter
{
	public partial class MainWindow : Window
	{
		private bool _activated;

		#region P/Invoke stuff

		[DllImport("user32.dll")]
		private extern static Int32 SetWindowLong(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);
		[DllImport("user32.dll")]
		private extern static Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);
		private const Int32 GWL_STYLE = -16;
		private const Int32 WS_MAXIMIZEBOX = 0x10000;

		#endregion

		public MainWindow()
		{
			InitializeComponent();

		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			if (!_activated)
			{
				_activated = true;

				// Disable the minimize button
				var hWnd = new WindowInteropHelper(this).Handle;
				var windowLong = GetWindowLong(hWnd, GWL_STYLE);
				windowLong = windowLong & ~WS_MAXIMIZEBOX;
				SetWindowLong(hWnd, GWL_STYLE, windowLong);
			}
		}
	}
}
