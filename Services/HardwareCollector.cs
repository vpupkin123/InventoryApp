using System;
using System.Collections.Generic;
using InventoryApp.Models;

namespace InventoryApp.Services
{
    public static class HardwareCollector
    {
        /// <summary>
        /// Collects all hardware information
        /// </summary>
        public static HardwareInfo Collect()
        {
            var result = new HardwareInfo
            {
                Computer = GetComputerInfo(),
                Motherboard = GetMotherboardInfo(),
                Cpu = GetCpuInfo(),
                Ram = GetRamInfo(),
                Storage = GetStorageInfo()
            };

            return result;
        }

        /// <summary>
        /// Gets computer model and serial number
        /// </summary>
        private static ComputerInfo GetComputerInfo()
        {
            var info = new ComputerInfo
            {
                Manufacturer = "N/A",
                Model = "N/A",
                SerialNumber = "N/A"
            };

            try
            {
                var data = WmiHelper.ExecuteQuery("SELECT * FROM Win32_ComputerSystem");
                if (data.Count > 0)
                {
                    var item = data[0];
                    info.Manufacturer = item.ContainsKey("Manufacturer") ? item["Manufacturer"]?.ToString() ?? "N/A" : "N/A";
                    info.Model = item.ContainsKey("Model") ? item["Model"]?.ToString() ?? "N/A" : "N/A";
                }

                // Try to get serial number from BIOS
                var biosData = WmiHelper.ExecuteQuery("SELECT * FROM Win32_BIOS");
                if (biosData.Count > 0)
                {
                    var bios = biosData[0];
                    info.SerialNumber = bios.ContainsKey("SerialNumber") ? bios["SerialNumber"]?.ToString() ?? "N/A" : "N/A";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get computer info: {ex.Message}");
            }

            return info;
        }

        /// <summary>
        /// Gets motherboard information
        /// </summary>
        private static MotherboardInfo GetMotherboardInfo()
        {
            var info = new MotherboardInfo
            {
                Manufacturer = "N/A",
                Model = "N/A",
                SerialNumber = "N/A"
            };

            try
            {
                var data = WmiHelper.ExecuteQuery("SELECT * FROM Win32_BaseBoard");
                if (data.Count > 0)
                {
                    var item = data[0];
                    info.Manufacturer = item.ContainsKey("Manufacturer") ? item["Manufacturer"]?.ToString() ?? "N/A" : "N/A";
                    info.Model = item.ContainsKey("Product") ? item["Product"]?.ToString() ?? "N/A" : "N/A";
                    info.SerialNumber = item.ContainsKey("SerialNumber") ? item["SerialNumber"]?.ToString() ?? "N/A" : "N/A";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get motherboard info: {ex.Message}");
            }

            return info;
        }

        /// <summary>
        /// Gets CPU information
        /// </summary>
        private static CpuInfo GetCpuInfo()
        {
            var info = new CpuInfo
            {
                Name = "N/A",
                Cores = 0,
                LogicalProcessors = 0,
                MaxClockSpeedMhz = 0
            };

            try
            {
                var data = WmiHelper.ExecuteQuery("SELECT * FROM Win32_Processor");
                if (data.Count > 0)
                {
                    var item = data[0];
                    info.Name = item.ContainsKey("Name") ? item["Name"]?.ToString() ?? "N/A" : "N/A";
                    info.Cores = item.ContainsKey("NumberOfCores") ? Convert.ToInt32(item["NumberOfCores"]) : 0;
                    info.LogicalProcessors = item.ContainsKey("NumberOfLogicalProcessors") ? Convert.ToInt32(item["NumberOfLogicalProcessors"]) : 0;
                    info.MaxClockSpeedMhz = item.ContainsKey("MaxClockSpeed") ? Convert.ToInt32(item["MaxClockSpeed"]) : 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get CPU info: {ex.Message}");
            }

            return info;
        }

        /// <summary>
        /// Gets RAM information (total + modules)
        /// </summary>
        private static RamInfo GetRamInfo()
        {
            var info = new RamInfo
            {
                TotalGb = 0,
                Modules = new List<RamModuleInfo>()
            };

            try
            {
                // Get total RAM
                var computerData = WmiHelper.ExecuteQuery("SELECT * FROM Win32_ComputerSystem");
                if (computerData.Count > 0)
                {
                    var computer = computerData[0];
                    if (computer.ContainsKey("TotalPhysicalMemory"))
                    {
                        long totalBytes = Convert.ToInt64(computer["TotalPhysicalMemory"]);
                        info.TotalGb = Math.Round(totalBytes / 1073741824.0, 2);
                    }
                }

                // Get RAM modules details
                var modulesData = WmiHelper.ExecuteQuery("SELECT * FROM Win32_PhysicalMemory");

                foreach (var item in modulesData)
                {
                    var module = new RamModuleInfo
                    {
                        Manufacturer = item.ContainsKey("Manufacturer") ? item["Manufacturer"]?.ToString() ?? "N/A" : "N/A",
                        PartNumber = item.ContainsKey("PartNumber") ? item["PartNumber"]?.ToString() ?? "N/A" : "N/A",
                        CapacityGb = item.ContainsKey("Capacity") ? Math.Round(Convert.ToDouble(item["Capacity"]) / 1073741824.0, 2) : 0,
                        SpeedMhz = item.ContainsKey("Speed") ? Convert.ToInt32(item["Speed"]) : 0,
                        Type = GetMemoryType(item),
                        FormFactor = GetFormFactor(item)
                    };
                    info.Modules.Add(module);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get RAM info: {ex.Message}");
            }

            return info;
        }

        /// <summary>
        /// Gets storage devices information
        /// </summary>
        private static List<StorageInfo> GetStorageInfo()
        {
            var storageList = new List<StorageInfo>();

            try
            {
                var data = WmiHelper.ExecuteQuery("SELECT * FROM Win32_DiskDrive");

                foreach (var item in data)
                {
                    var storage = new StorageInfo
                    {
                        Model = item.ContainsKey("Model") ? item["Model"]?.ToString() ?? "N/A" : "N/A",
                        SizeGb = item.ContainsKey("Size") ? Math.Round(Convert.ToDouble(item["Size"]) / 1073741824.0, 2) : 0,
                        InterfaceType = item.ContainsKey("InterfaceType") ? item["InterfaceType"]?.ToString() ?? "N/A" : "N/A",
                        MediaType = item.ContainsKey("MediaType") ? item["MediaType"]?.ToString() ?? "N/A" : "N/A"
                    };
                    storageList.Add(storage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get storage info: {ex.Message}");
            }

            return storageList;
        }

        /// <summary>
        /// Converts SMBIOSMemoryType to readable string
        /// </summary>
        private static string GetMemoryType(Dictionary<string, object> item)
        {
            if (!item.ContainsKey("SMBIOSMemoryType"))
                return "N/A";

            int type = Convert.ToInt32(item["SMBIOSMemoryType"]);

            switch (type)
            {
                case 20: return "DDR";
                case 21: return "DDR2";
                case 24: return "DDR3";
                case 26: return "DDR4";
                case 34: return "DDR5";
                default: return $"Type {type}";
            }
        }

        /// <summary>
        /// Converts FormFactor to readable string
        /// </summary>
        private static string GetFormFactor(Dictionary<string, object> item)
        {
            if (!item.ContainsKey("FormFactor"))
                return "N/A";

            int formFactor = Convert.ToInt32(item["FormFactor"]);

            switch (formFactor)
            {
                case 8: return "DIMM";
                case 12: return "SODIMM";
                default: return $"Form {formFactor}";
            }
        }
    }
}