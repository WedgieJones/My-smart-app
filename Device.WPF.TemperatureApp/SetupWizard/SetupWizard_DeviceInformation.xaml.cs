﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Device.WPF.TemperatureApp.Models;

namespace Device.WPF.TemperatureApp.SetupWizard
{
	/// <summary>
	/// Interaction logic for SetupWizard_DeviceInformation.xaml
	/// </summary>
	public partial class SetupWizard_DeviceInformation : Page
	{
		public DeviceInformation _deviceInformation;
		public SetupWizard_DeviceInformation(ref DeviceInformation deviceInformation)
		{
			InitializeComponent();
			_deviceInformation = deviceInformation;
			tb_DeviceName.Text = _deviceInformation.DeviceName;
			tb_Location.Text = _deviceInformation.Location;
		}

		private void Tb_DeviceName_OnKeyUp(object sender, KeyEventArgs e)
		{
			_deviceInformation.DeviceName = tb_DeviceName.Text;
		}

		private void Tb_Location_OnKeyUp(object sender, KeyEventArgs e)
		{
			_deviceInformation.Location = tb_Location.Text;
		}
	}
}