using System.Collections.Generic;

namespace InventoryApp.Models
{
    public class InventoryReport
    {
        public UserInfo User { get; set; }
        public SystemInfo System { get; set; }
        public HardwareInfo Hardware { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class UserInfo
    {
        public string FullName { get; set; }
        public string FileName { get; set; }
        public string Timestamp { get; set; }
    }

    public class SystemInfo
    {
        public string ComputerName { get; set; }
        public OsInfo Os { get; set; }
        public NetworkInfo Network { get; set; }
    }

    public class OsInfo
    {
        public string Caption { get; set; }
        public string Version { get; set; }
        public string BuildNumber { get; set; }
    }

    public class NetworkInfo
    {
        public string IpAddress { get; set; }
    }

    public class HardwareInfo
    {
        public ComputerInfo Computer { get; set; }
        public MotherboardInfo Motherboard { get; set; }
        public CpuInfo Cpu { get; set; }
        public RamInfo Ram { get; set; }
        public List<StorageInfo> Storage { get; set; }
    }

    public class ComputerInfo
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
    }

    public class MotherboardInfo
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
    }

    public class CpuInfo
    {
        public string Name { get; set; }
        public int Cores { get; set; }
        public int LogicalProcessors { get; set; }
        public int MaxClockSpeedMhz { get; set; }
    }

    public class RamInfo
    {
        public double TotalGb { get; set; }
        public List<RamModuleInfo> Modules { get; set; }
    }

    public class RamModuleInfo
    {
        public string Manufacturer { get; set; }
        public string PartNumber { get; set; }
        public double CapacityGb { get; set; }
        public int SpeedMhz { get; set; }
        public string Type { get; set; }
        public string FormFactor { get; set; }
    }

    public class StorageInfo
    {
        public string Model { get; set; }
        public double SizeGb { get; set; }
        public string InterfaceType { get; set; }
        public string MediaType { get; set; }
    }
}