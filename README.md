# 💻 Inventory Web Client

Desktop application for collecting hardware information and generating JSON reports for Inventory Web system.

## About the Application

**Inventory Web Client** is a lightweight desktop application that automatically collects detailed information about your computer's hardware, operating system, and network configuration. It generates a JSON report file that can be uploaded to the Inventory Web system for IT asset management.

### What it collects:

*   Computer manufacturer and model
*   Motherboard information and serial number
*   Processor details (model, cores, threads, clock speed)
*   RAM modules (capacity, speed, type)
*   Storage devices (excluding USB and removable drives)
*   Operating system version and build
*   Network configuration (IP address, computer name)

**💡 Privacy:** The application collects only hardware and system information. No personal files, documents, or sensitive data are accessed or transmitted.

## Download

Choose the version for your operating system:

🪟

#### Windows

Windows 7 or higher

[Download .exe](https://github.com/vpupkin123/InventoryApp/releases/download/v1.0.0/v1.0.0.zip)

🍎


### Windows

1

Download **inventory-client.exe** from the section above

2

Double-click the file to run it (no installation required)

3

The application will automatically collect hardware information

4

A JSON report file will be created in the same folder (named `report_*.json`)

5

Upload this file through the Inventory Web interface


Upload the generated JSON file to Inventory Web


## System Requirements

### Windows

*   Windows 7, 8, 10, or 11
*   No additional software required
*   Administrator rights not required (but recommended for complete data collection)

## Output Format

The application generates a JSON file with the following structure:

*   **User information:** Full name, filename, timestamp
*   **System information:** Computer name, OS version, IP address
*   **Hardware information:** Complete details about motherboard, CPU, RAM, and storage devices

**💡 File naming:** Reports are automatically named with the pattern `report_[timestamp].json` to avoid overwriting previous reports.

## Support

If you encounter any issues or have questions:

*   Check the main project documentation: [User Guide](../docs/en_user-guide.html)
*   Create an issue in the main repository: [github.com/vpupkin123/inventory-web](https://github.com/vpupkin123/inventory-web)
*   Contact the developer: [vpupkin123](https://github.com/vpupkin123)

**Inventory Web Client** — Part of the [Inventory Web](https://github.com/vpupkin123/inventory-web) project

Developed by [vpupkin123](https://github.com/vpupkin123)