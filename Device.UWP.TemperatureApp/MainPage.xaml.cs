using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Azure.Devices;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Device.UWP.TemperatureApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ServiceClient 
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BtnDirectMethod_OnClick(object sender, RoutedEventArgs e)
        {
	        var methodInvoke = new CloudToDeviceMethodResult("GetDeviceName");
            var result = await ServiceClient.InvokeDeviceMethodAsync("",)
        }

        private async Task InvokeDirectMethodAsync(string deviceId)
        {

        } 
    }
}
