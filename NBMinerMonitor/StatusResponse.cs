using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NBMinerMonitor
{
    public partial class StatusResponse
    {
        [JsonProperty("miner")]
        public Miner Miner { get; set; }

        [JsonProperty("reboot_times")]
        public long RebootTimes { get; set; }

        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        [JsonProperty("stratum")]
        public Stratum Stratum { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public partial class Miner
    {
        [JsonProperty("devices")]
        public Device[] Devices { get; set; }

        [JsonProperty("total_hashrate")]
        public string TotalHashrate { get; set; }

        [JsonProperty("total_hashrate2")]
        public string TotalHashrate2 { get; set; }

        [JsonProperty("total_hashrate2_raw")]
        public long TotalHashrate2Raw { get; set; }

        [JsonProperty("total_hashrate_raw")]
        public double TotalHashrateRaw { get; set; }

        [JsonProperty("total_power_consume")]
        public long TotalPowerConsume { get; set; }
    }

    public partial class Device
    {
        [JsonProperty("accepted_shares")]
        public long AcceptedShares { get; set; }

        [JsonProperty("core_clock")]
        public long CoreClock { get; set; }

        [JsonProperty("core_utilization")]
        public long CoreUtilization { get; set; }

        [JsonProperty("fan")]
        public long Fan { get; set; }

        [JsonProperty("hashrate")]
        public string Hashrate { get; set; }

        [JsonProperty("hashrate2")]
        public string Hashrate2 { get; set; }

        [JsonProperty("hashrate2_raw")]
        public long Hashrate2Raw { get; set; }

        [JsonProperty("hashrate_raw")]
        public double HashrateRaw { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("invalid_shares")]
        public long InvalidShares { get; set; }

        [JsonProperty("mem_clock")]
        public long MemClock { get; set; }

        [JsonProperty("mem_utilization")]
        public long MemUtilization { get; set; }

        [JsonProperty("pci_bus_id")]
        public long PciBusId { get; set; }

        [JsonProperty("power")]
        public long Power { get; set; }

        [JsonProperty("rejected_shares")]
        public long RejectedShares { get; set; }

        [JsonProperty("temperature")]
        public long Temperature { get; set; }
    }

    public partial class Stratum
    {
        [JsonProperty("accepted_shares")]
        public long AcceptedShares { get; set; }

        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("dual_mine")]
        public bool DualMine { get; set; }

        [JsonProperty("invalid_shares")]
        public long InvalidShares { get; set; }

        [JsonProperty("latency")]
        public long Latency { get; set; }

        [JsonProperty("pool_hashrate_10m")]
        public string PoolHashrate10M { get; set; }

        [JsonProperty("pool_hashrate_24h")]
        public string PoolHashrate24H { get; set; }

        [JsonProperty("pool_hashrate_4h")]
        public string PoolHashrate4H { get; set; }

        [JsonProperty("rejected_shares")]
        public long RejectedShares { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("use_ssl")]
        public bool UseSsl { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }
}
