using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;
using Device.WPF.TemperatureApp.Models;
using Device.WPF.TemperatureApp.Services;

namespace Device.WPF.TemperatureApp.SetupWizard
{
	enum SetupWizardSteps
	{
		YourInformation,
		DeviceInformation,
		ConnectionInformation,
		Finish
	}
	/// <summary>
	/// Interaction logic for SetupWizard.xaml
	/// </summary>
	public partial class SetupWizard : Window
	{
		private  readonly ConfigurationService configurationService;
		private SetupWizardSteps currentStep;
		public YourInformation yourInformation;
		public DeviceInformation deviceInformation;
		public ConnectionInformation connectionInformation;
		


		public SetupWizard()
		{
			InitializeComponent();
			yourInformation = new YourInformation();
			deviceInformation = new DeviceInformation();
			connectionInformation = new ConnectionInformation();
			configurationService = new ConfigurationService();
			currentStep = SetupWizardSteps.YourInformation;
			currentPage.Content = new SetupWizard_YourInformation(ref yourInformation);

		}

		private void Btn_Back_OnClick(object sender, RoutedEventArgs e)
		{
			switch (currentStep)
			{
				
				case SetupWizardSteps.ConnectionInformation:
					currentStep = SetupWizardSteps.DeviceInformation;
					currentPage.Content = new SetupWizard_DeviceInformation(ref deviceInformation);
					tblock_Next.Text = "Next";
					tblock_DeviceInformation.FontWeight = FontWeights.Bold;
					tblock_Connect.FontWeight = FontWeights.Normal;
					ellipse_DeviceInformation.Fill = new SolidColorBrush(Colors.LightBlue);
					ellipse_Connect.Fill = new SolidColorBrush(Colors.LightGray);


					break;

				case SetupWizardSteps.DeviceInformation:
					currentStep = SetupWizardSteps.YourInformation;
					currentPage.Content = new SetupWizard_YourInformation(ref yourInformation);
					btn_Back.Visibility = Visibility.Hidden;

					tblock_DeviceInformation.FontWeight = FontWeights.Normal;
					tblock_YourInformation.FontWeight = FontWeights.Bold;
					ellipse_DeviceInformation.Fill = new SolidColorBrush(Colors.LightGray);
					ellipse_YourInformation.Fill = new SolidColorBrush(Colors.LightBlue);
					break;
			}
		}

		private async void Btn_Next_OnClick(object sender, RoutedEventArgs e)
		{
			switch (currentStep)
			{
				case SetupWizardSteps.YourInformation:
					currentStep = SetupWizardSteps.DeviceInformation;
					currentPage.Content = new SetupWizard_DeviceInformation(ref deviceInformation);
					btn_Back.Visibility = Visibility.Visible;
					tblock_DeviceInformation.FontWeight = FontWeights.Bold;
					tblock_YourInformation.FontWeight = FontWeights.Normal;
					ellipse_DeviceInformation.Fill = new SolidColorBrush(Colors.LightBlue);
					ellipse_YourInformation.Fill = new SolidColorBrush(Colors.LightGray);
					break;

				case SetupWizardSteps.DeviceInformation:
					currentStep = SetupWizardSteps.ConnectionInformation;
					currentPage.Content = new SetupWizard_Connection(ref connectionInformation);
					tblock_Next.Text = "Finish";

					tblock_DeviceInformation.FontWeight = FontWeights.Normal;
					tblock_Connect.FontWeight = FontWeights.Bold;
					ellipse_DeviceInformation.Fill = new SolidColorBrush(Colors.LightGray);
					ellipse_Connect.Fill = new SolidColorBrush(Colors.LightBlue);

					break;

				case SetupWizardSteps.ConnectionInformation:
					currentStep = SetupWizardSteps.Finish;
					break;

				case SetupWizardSteps.Finish:
					await ConfigureDeviceAsync();
					break;
			}
		}

		private async Task ConfigureDeviceAsync()
		{
			deviceInformation = await configurationService.ConnectDeviceAsync(
				$"{connectionInformation.ConnectionEndpoint}?deviceId={deviceInformation.DeviceId}", deviceInformation);
			if (!string.IsNullOrEmpty(deviceInformation.ConnectionString))
			{
				deviceInformation.IsConfigured = true;
				await configurationService.SaveYourInformationAsync(yourInformation);
				await configurationService.SaveDeviceInformationAsync(deviceInformation);

				Hide();
				MainWindow mainWindow = new MainWindow();
				mainWindow.Show();
			}
		}
	}
}
