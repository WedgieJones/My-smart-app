using SmartApp.MVVM.ViewModels;
using SmartApp.Services;
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

namespace SmartApp.Components
{
	/// <summary>
	/// Interaction logic for Temperature.xaml
	/// </summary>
	public partial class Temperature : UserControl
	{
		private readonly ITemperatureService _tempService;
		public Temperature()
		{
			InitializeComponent();

			_tempService = new TemperatureService();
			this.DataContext = new TempComponentViewModel(_tempService);

		}
	}
}
