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
using Device.WPF.TemperatureApp.Models;

namespace Device.WPF.TemperatureApp.SetupWizard
{
	/// <summary>
	/// Interaction logic for SetupWizard_Connection.xaml
	/// </summary>
	public partial class SetupWizard_Connection : Page
	{

		public ConnectionInformation _connectionInformation;
		public SetupWizard_Connection(ref ConnectionInformation connectionInformation)
		{
			InitializeComponent();
			_connectionInformation = connectionInformation;
			tb_ConnectionEndpoint.Text = _connectionInformation.ConnectionEndpoint;
		}


		private void Tb_ConnectionEndpoint_OnKeyUp(object sender, KeyEventArgs e)
		{
			_connectionInformation.ConnectionEndpoint = tb_ConnectionEndpoint.Text;
		}
	}
}
