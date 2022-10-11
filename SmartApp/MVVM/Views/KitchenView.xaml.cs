using System;
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
using Microsoft.Azure.Devices;
using SmartApp.MVVM.Models;

namespace SmartApp.MVVM.Views
{
	/// <summary>
	/// Interaction logic for KitchenView.xaml
	/// </summary>
	public partial class KitchenView : UserControl
	{
		private readonly RegistryManager _registryManager = RegistryManager.CreateFromConnectionString("HostName=Fredriks-IoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=oTaXRPi71jD6g3fi11SX0GUcrlnMq9IeJWpPaV/utSQ=");
		private List<DeviceItem> devices = new List<DeviceItem>();
		public KitchenView()
		{
			InitializeComponent();
		}

		


		private void BtnClose_OnClick(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
