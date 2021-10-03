using NBMinerMonitor.Misc;
using NBMinerMonitor.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data;

namespace NBMinerMonitor.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public List<string> endpoints = new List<string>();
        public Timer updateTimer = new Timer(1000);

        public ObservableCollection<GpuInfo> GPUs_ { get; set; } = new ObservableCollection<GpuInfo>();
        public ListCollectionView GPUs { get; set; } 

        public string RigAddressToAdd { get; set; }
        public string RigPortToAdd { get; set; } = "22333";

        private double totalHashrate;
        public double TotalHashrate
        {
            get
            {
                return Math.Round(totalHashrate, 2);
            }
            set
            {
                totalHashrate = value;
                OnPropertyChanged(nameof(TotalHashrate));
                OnPropertyChanged(nameof(HashrateHeader));
            }
        }

        public string HashrateHeader
        {
            get
            {
                return $"Hashrate (Mhs {TotalHashrate})";
            }
        }

        public ICommand AddIpCommand { get; set; }

        public MainWindowViewModel()
        {
            endpoints = Globals.Endpoints;

            GPUs = new ListCollectionView(GPUs_);
            GPUs.GroupDescriptions.Add(new PropertyGroupDescription(nameof(GpuInfo.Ip)));

            AddIpCommand = new RelayCommand((s) =>
            {
                var newEndpoint = $"{RigAddressToAdd}:{RigPortToAdd}";
                lock(endpoints)
                {
                    if (!endpoints.Contains(newEndpoint))
                    {
                        endpoints.Add(newEndpoint);
                        Globals.Endpoints = endpoints;
                    }
                }
            });

            updateTimer.Elapsed += async (s, e) =>
            {
                if (!endpoints.Any()) return;

                var bag = new ConcurrentBag<(string, StatusResponse)>();

                var tasks = endpoints.ToArray().Select(async item =>
                {
                    try
                    {
                        StatusResponse response = await GetStatus(item);
                        if(response != null)
                        {
                            bag.Add((item, response));
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                });
                
                await Task.WhenAll(tasks);

                Application.Current.Dispatcher.Invoke(() => GPUs_.Clear());

                var tmpGpusInfoQuery = bag.SelectMany(x =>
                {
                    return x.Item2.Miner.Devices.Select(u => new GpuInfo()
                    {
                        Ip = x.Item1,
                        Device = u
                    });
                });

                foreach(var gpuInfo in tmpGpusInfoQuery)
                {
                    Application.Current.Dispatcher.Invoke(() => GPUs_.Add(gpuInfo));
                }

                TotalHashrate = tmpGpusInfoQuery.Sum(x => x.Device.HashrateRaw / 1000000);
            };

            updateTimer.Start();
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
            var client = new RestClient($"http://{ip}/api/v1/status");
            client.Timeout = 100;
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<StatusResponse>(response.Content);
        }
    }
}
