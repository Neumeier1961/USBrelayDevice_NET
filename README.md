# USBrelayDevice_NET
A C# library that provides methods to control HID compliant USB Relay Devices. The library uses native function calls to standard Windows APIs for communicating with the USB Relay devices.
Note: This mis NOT a wrapper for the usb_relay_device.dll, but a .NET alternative for controlling the same devices.
#
Only one USB Relay device can opened and controlled with each instance of the class, to open multiple devices connected to a system, a new instance of the class must be created for each device.
## Features
* Gets a list containing information of all USB Relay Devices attached to system
* Methods for controlling relay states
* Set/Edit device ID/serial number
* Error event
* No third party libraries required
* Class can be added to a project or compiled as a class library
### Notes
References Used: Sysytem and System.Core
#
Native Libraries used: hid.dll, setupapi.dll and kernel32.dll
# 
.NET Framework: 4.5, Build platform: x86, x64 or AnyCPU
# 
Libarary has been tested in applications compiled for 32 bit and 64 bit CPUs in Windows 7/10/11
# 
Built using Visual Studio 2014 Pro and Visual Studio 2022 Pro
