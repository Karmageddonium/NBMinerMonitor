using NBMinerMonitor.ViewModels;
using Newtonsoft.Json;
using RestSharp;
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

namespace NBMinerMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
            //Monitor();
        }

        public async void Monitor()
        {
            List<StatusResponse> responses = new List<StatusResponse>();

            var tstatus = await GetStatus("http://192.168.88.221:22333/api/v1/status");
            Parallel.For(0, 256, (i) =>
            {
                var status = GetStatus($"192.168.88.{i}").GetAwaiter().GetResult();
                lock (responses)
                {
                    responses.Add(status);
                }
            });
        }

        public async Task<StatusResponse> GetStatus(string ip)
        {
            var client = new RestClient("http://192.168.88.221:22333/api/v1/status");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<StatusResponse>(response.Content);
        }
    }
}
