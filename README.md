# WiFi Strength App

A .NET 10 Windows application that scans and displays WiFi networks with their signal strength.

## Features

- Lists all available WiFi networks
- Shows signal strength as percentage and visual bars
- Displays authentication type
- Highlights currently connected network
- Supports continuous monitoring mode

## Requirements

- Windows OS
- .NET 10 SDK
- Administrator privileges (required for WiFi scanning)

## Build

```bash
dotnet build
```

## Run

**Single scan:**
```bash
dotnet run
```

**Continuous monitoring mode:**
```bash
dotnet run -- --watch
```
or
```bash
dotnet run -- -w
```

## Output Example

```
WiFi Network Scanner
===================

Found 5 network(s):

SSID                                Signal %   Signal Bar      Auth Type 
---------------------------------------------------------------------------
MyHomeNetwork                       95         █████░░░░░      WPA2_PSK  
CoffeeShop_Guest                    78         ████░░░░░░      Open      
Neighbor_WiFi                       45         ███░░░░░░░      WPA2_PSK  
Office_5G                           32         ██░░░░░░░░      WPA2_EAP  
WeakSignal                          15         █░░░░░░░░░      WPA2_PSK  

Currently connected to: MyHomeNetwork
```

## Notes

- Must be run as Administrator to access WiFi APIs
- Signal quality ranges from 0-100%
- Visual bars show signal strength (5 bars = excellent, 1 bar = poor)
