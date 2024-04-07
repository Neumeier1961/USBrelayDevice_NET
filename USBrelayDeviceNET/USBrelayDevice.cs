//  Copyright (c) 2024, Advanced Chemical Solutions LLC
//  Written by: David Neumeier, Email:  david.neumeier@adv-chem.com
//  All rights reserved.

//  Redistribution and use in source and binary forms, with or without modification, 
//  are permitted provided that the following conditions are met:

//  Redistributions of source code must retain the above copyright notice, 
//  this list of conditions and the following disclaimer. 
//  Redistributions in binary form must reproduce the above copyright notice, 
//  this list of conditions and the following disclaimer in the documentation 
//  and/or other materials provided with the distribution. 

//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.

/*USB Relay Device HID specifications

 *Vendor ID = 0x16C0
 *Product ID = 0x05DF

 *Product Name = USBRelay#, where # = number of realys on the device

 *HID Get Feature, ReportBuffer parameter:
 ReportBuffer definition; byte[] GetBuffer = new byte[9], 9 element byte array
 GetBuffer[0] = 0x00, report number
 GetBuffer[1] = first char of ID string, cast to char
 GetBuffer[2] = second char of ID string, cast to char
 GetBuffer[3] = third char of ID string, cast to char
 GetBuffer[4] = fourth char of ID string, cast to char
 GetBuffer[5] = fith char of ID string, cast to char
 GetBuffer[6] = 0x00, not used
 GetBuffer[7] = 0x00, not used
 GetBuffer[8] = relay_states, convert to an 8 element bit array, var bitarry = new BitArray(new[] {relay_states});

 *HID Set Feature ReportBuffer parameter: (there are 5 set command options, 0xFA, 0xFC, 0xFD, 0xFE and 0xFF)
 ReportBuffer definition; byte[] SetBuffer = new byte[9], 9 element byte array

 command option 1 -- 0xFA; Set Device ID/serial number (string having a length of 5 characters)
 SetBuffer[0] = 0x00, report number
 SetBuffer[1] = 0xFA, command
 SetBuffer[2] = first char of ID string, cast to byte
 SetBuffer[3] = second char of ID string, cast to byte
 SetBuffer[4] = third char of ID string, cast to byte
 SetBuffer[5] = fourth char of ID string, cast to byte
 SetBuffer[6] = fith char of ID string, cast to byte
 SetBuffer[7] = 0x00, not used
 SetBuffer[8] = 0x00, not used

 command option 2 -- 0xFC; Set all relays states off
 SetBuffer[0] = 0x00, report number
 SetBuffer[1] = 0xFC, command
 SetBuffer[2] = 0x00, relay number set to 0
 SetBuffer[3] = 0x00, not used
 SetBuffer[4] = 0x00, not used
 SetBuffer[5] = 0x00, not used
 SetBuffer[6] = 0x00, not used
 SetBuffer[7] = 0x00, not used
 SetBuffer[8] = 0x00, not used

 command option 3 -- 0xFD; Set single relay state off
 SetBuffer[0] = 0x00, report number
 SetBuffer[1] = 0xFD, command
 SetBuffer[2] = n, relay number (cannot be greater than number of relays on device)
 SetBuffer[3] = 0x00, not used
 SetBuffer[4] = 0x00, not used
 SetBuffer[5] = 0x00, not used
 SetBuffer[6] = 0x00, not used
 SetBuffer[7] = 0x00, not used
 SetBuffer[8] = 0x00, not used

 command option 4 -- 0xFE; Set all relays states on
 SetBuffer[0] = 0x00, report number
 SetBuffer[1] = 0xFE, command
 SetBuffer[2] = 0x00, relay number set to 0
 SetBuffer[3] = 0x00, not used
 SetBuffer[4] = 0x00, not used
 SetBuffer[5] = 0x00, not used
 SetBuffer[6] = 0x00, not used
 SetBuffer[7] = 0x00, not used
 SetBuffer[8] = 0x00, not used

 command option 5 -- 0xFF; Set single relay state on
 SetBuffer[0] = 0x00, report number
 SetBuffer[1] = 0xFF, command
 SetBuffer[2] = n, relay number (cannot be greater than number of relays on device)
 SetBuffer[3] = 0x00, not used
 SetBuffer[4] = 0x00, not used
 SetBuffer[5] = 0x00, not used
 SetBuffer[6] = 0x00, not used
 SetBuffer[7] = 0x00, not used
 SetBuffer[8] = 0x00, not used
 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace USBrelayDeviceNET
{
    /// <summary>
    /// Provides control of HID compliant USB Relay Devices
    /// The class uses standard calls to native functions in Win32 APIs
    /// for communicating with the USB Relay devices.
    /// </summary>
    /// <remarks>
    /// Only one USB Relay device can opened and controlled with each instance of the class, to open multiple
    /// devices connected to a system, a new instance of the class must be created for each device.
    /// </remarks>
    /// References: Sysytem and System.Core
    /// Native Libraries used: hid.dll, setupapi.dll and kernel32.dll
    /// .NET Framework: 4.5, Build platform: x86, x64 or AnyCPU
    /// Libarary has been tested in applications compiled for 32 bit and 64 bit CPUs in Windows 7/10/11
    /// Built using Visual Studio 2014 Pro and using Visual Studio 2022 Pro
    public sealed class USBrelayDevice: IDisposable
    {
        #region class constructer/destructor

        /// <summary>
        /// Initializes a new instance of USBrelayDevice class
        /// </summary>
        public USBrelayDevice()
        {
            pDevice = new IntPtr();
            IsDisposed = false;
            RelayStatus = new bool[8];
            GetUSBrelayDevices(true);
        }

        /// <summary>
        /// Initializes a new instance of USBrelayDevice class
        /// and opens a USB Relay device matching the device_ID
        /// </summary>
        /// <param name="device_ID">ID/serial number of device to open</param>
        /// <remarks>
        /// Typically used when multiple device are attached to the system and
        /// a specific device should be opened when the class is initialized
        /// </remarks>
        public USBrelayDevice(string device_ID)
        {
            pDevice = new IntPtr();
            IsDisposed = false;
            RelayStatus = new bool[8];
            GetUSBrelayDevices(true);
            if (DevicesFound)
            {
                OpenDevice(device_ID);
            }
        }

        /// <summary>
        /// Initializes a new instance of USBrelayDevice class
        /// and opens the first USB Relay Device found
        /// </summary>
        /// <param name="open_First">true to open the device, false if no device should be opened</param>
        /// <remarks>
        /// Typically used when only one device is attached to the system
        /// </remarks>
        public USBrelayDevice(bool open_First)
        {
            pDevice = new IntPtr();
            IsDisposed = false;
            RelayStatus = new bool[8];
            GetUSBrelayDevices(true);
            if (DevicesFound & open_First)
            {
                OpenDevice(0);
            }
        }

        /// <summary>
        /// Class finalizer
        /// </summary>
        ~USBrelayDevice()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by USBrelayDevice.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by USBrelayDevice and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (disposing)
            {
                if (DeviceOpen | pDevice != IntPtr.Zero) CloseDevice();
                localDeviceList.Clear();
            }           
            pDevice = IntPtr.Zero;
            DeviceOpen = false;
            DevicesFound = false;
            IsDisposed = true;
        }

        #endregion

        #region Error Event Handler

        /// <summary>
        /// Error Event Arguments
        /// </summary>
        public class USBrelayEventArgs : EventArgs
        {
            /// <summary>Error Description</summary>
            public string Error { get; set; }

            /// <summary>Method throwing error</summary>
            public string Source { get; set; }

            /// <summary>true if error is an exception type</summary>
            public bool IsException { get; set; }

            /// <summary>if error is exception type, then exception thrown else null</summary>
            public Exception ExceptionThrown { get; set; }
        }

        /// <summary>
        /// Error event handler
        /// </summary>
        /// <param name="e">error event arguments</param>
        private void OnError(USBrelayEventArgs e)
        {
            var handler = USBrelayError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// USBrelayDevice class Error Event
        /// </summary>
        public event EventHandler<USBrelayEventArgs> USBrelayError;

        /// <summary>
        /// Common method to fire Error event
        /// </summary>
        /// <param name="error">Error to report</param>
        /// <param name="source">Method name with error</param>
        /// <param name="exception">If error is Exception, then exception thrown, else null</param>
        /// <param name="win32_error">native error code</param>
        private void Error_Handler(string error, string source, object exception, uint win32_error)
        {
            if (!win32_error.Equals(0))
            {
                var err = new Win32Exception((int) win32_error);
                var _error = new USBrelayEventArgs
                {
                    ExceptionThrown = null,
                    Error = error + " - " + win32_error + " : " + err.Message,
                    Source = source,
                    IsException = false
                };

                OnError(_error);
            }
            else
            {
                var _error = new USBrelayEventArgs
                {
                    ExceptionThrown = (Exception)exception,
                    Error = error,
                    Source = source,
                    IsException = (exception != null)
                };

                OnError(_error);
            }
        }

        #endregion

        #region Public Properties

        /// <summary> Contains USB Relay device information, this structure is returned in a list using the GetDevices() method</summary>
        public struct DeviceInfo
        {
            /// <summary> Device serial number</summary>
            public string device_ID;

            /// <summary> System path to device</summary>
            public string device_path;

            /// <summary> Number of relay channels</summary>
            public int relay_Count;

            /// <summary> Device description</summary>
            public string product_name;

            /// <summary> List index value</summary>
            public int index;
        }

        /// <summary> Tue when class has been disposed</summary>
        public bool IsDisposed { get; private set; }

        /// <summary> True when USB Relay devices are found and available for connection</summary>
        public bool DevicesFound { get; private set; }

        /// <summary> True when a USB Relay device is connected</summary>
        public bool DeviceOpen { get; private set; }

        /// <summary> Array of current relay states, true = ON and false = OFF</summary>
        public bool[] RelayStatus { get; private set; }

        #endregion

        #region Private Members

        /// <summary> USB Relay Device Information</summary>
        private struct LocalDeviceInfo
        {
            public string device_path;
            public string device_ID;
            public int relay_Count;
        }

        /// <summary>Device Information list of all USB Relay Devices attached to system</summary>
        private List<LocalDeviceInfo> localDeviceList;

        /// <summary> Relay count of current open device</summary>
        private int relay_count;

        /// <summary> Handle of current open device</summary>
        private IntPtr pDevice;

        /// <summary> set HID feature flag</summary>
        private const int SET = - 0x00;

        /// <summary> get HID feature flag</summary>
        private const int GET = 0x01;

        /// <summary> wait for method to complete flag</summary>
        private bool wait_for_completion;

        /// <summary> set relay state on flag</summary>
        private const bool ON = true;

        /// <summary>  set relay state off flag</summary>
        private const bool OFF = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds all USB Relay Devices attached to the system and
        /// returns a list containing device information for each of the devices.
        /// </summary>
        /// <returns>list of device information structures, if no devices will return an empty list</returns>
        public List<DeviceInfo> GetUSBrelayDevices(bool closeOpenDevice)
        {
            while (wait_for_completion){}

            wait_for_completion = true;

            // Close device if open
            if (pDevice != IntPtr.Zero & closeOpenDevice)
            {
                CloseDevice();
            }

            // constants used to find specific HID devices
            const ushort VID = 0x16c0; // USB Relay Device Vendor ID
            const ushort PID = 0x05df; // USB Relay Device Product ID

            // initialize public and private class members set within method
            var DeviceInfoList = new List<DeviceInfo>(); //return List
            localDeviceList = new List<LocalDeviceInfo>(); //internal class list
            DevicesFound = false;

            // get paths to USB Relay Devices
            var devicePaths = HID_GetDevicePaths(VID, PID);

            // if no devices found end method
            if (devicePaths.Length == 0)
            {
                wait_for_completion = false;
                return DeviceInfoList;
            }

            // loop variable declarations
            var index = 0; // device information list indexer
            var pProductName = new IntPtr(); // pointer to device product name
            var pHandle = new IntPtr(); // pointer to device handle

            foreach (var devicePath in devicePaths)
            {
                try
                {
                    //open USB relay device
                    pHandle = HID_OpenDdevice(devicePath, true);

                    //if failed to get device handle, skip device
                    if(pHandle == IntPtr.Zero) continue;

                    // get product name
                    var product_name = "";
                    pProductName = Marshal.AllocCoTaskMem(sizeof(char) * 128);
                    var result = NativeMethods.HidD_GetProductString(
                        pHandle,
                        pProductName,
                        sizeof(char) * 128);

                    if (result) product_name = Marshal.PtrToStringAuto(pProductName);

                    // if fails skip device
                    if (string.IsNullOrEmpty(product_name) || !product_name.ToUpper().Contains("USBRELAY")) continue;

                    // Get relay count of device (last char of Product Name string)
                    var rly_count = Convert.ToInt32(product_name.Remove(0, 8));

                    // if fails skip device
                    if (rly_count <= 0 | rly_count > 8) continue;

                    // get device ID string, if fails ID will return as default value of "error"
                    var idStr = "error";
                    var idBuffer = new byte[9];
                    result = NativeMethods.HidD_GetFeature(pHandle, idBuffer, idBuffer.Length);

                    if (result)
                    {
                        idStr = new string(new[]
                        {
                            (char)idBuffer[1], 
                            (char)idBuffer[2], 
                            (char)idBuffer[3], 
                            (char)idBuffer[4], 
                            (char)idBuffer[5]
                        });
                    }

                    // create device information structures
                    var devInfo = new DeviceInfo
                    {
                        device_path = devicePath,
                        device_ID = idStr,
                        product_name = product_name,
                        relay_Count = rly_count,
                        index = index
                    };

                    var localDev = new LocalDeviceInfo
                    {
                        device_path = devicePath,
                        device_ID = idStr,
                        relay_Count = rly_count
                    };

                    // update lists
                    DeviceInfoList.Add(devInfo);
                    localDeviceList.Add(localDev);

                    // advance list index for next USB Relay Device
                    index++;
                }
                catch (Exception ex)
                {
                    Error_Handler("Exception", "GetUSBrelayDevices()", ex, 0);
                }
                finally
                {
                    // free resources and reset pointers for next iteration
                    if (pHandle != IntPtr.Zero)
                    {
                        NativeMethods.CloseHandle(pHandle);
                        pHandle = IntPtr.Zero;
                    }

                    if (pProductName != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(pProductName);
                        pProductName = IntPtr.Zero;
                    }
                }
            }

            // Set flags and return device list
            wait_for_completion = false;
            DevicesFound = (DeviceInfoList.Count > 0);
            return DeviceInfoList;
        }

        /// <summary>
        /// Opens a USB Relay Device using list index
        /// </summary>
        /// <param name="device_index">index of device to open in device list</param>
        /// <returns>true if success, false if failed</returns>
        public bool OpenDevice(int device_index)
        {
            //if device is open, close device
            if (pDevice != IntPtr.Zero)
            {
                CloseDevice();
            }

            //initialize device open flag and relay count
            DeviceOpen = false;
            relay_count = 0;

            //verify device available to open
            if (localDeviceList.Count == 0 | device_index >= localDeviceList.Count)
            {
                Error_Handler("Device index not valid or no devices.",
                    "OpenDevice(int)", null, 0);
                return false;
            }

            //open device using device path and set device handle pointer
            pDevice = HID_OpenDdevice(localDeviceList[device_index].device_path, false);

            //set device open flag
            DeviceOpen = (pDevice != IntPtr.Zero);

            //set device relay count
            if (DeviceOpen)
            {
                relay_count = localDeviceList[device_index].relay_Count;
                UpdateRelayStatus();
            }

            return DeviceOpen;
        }

        /// <summary>
        /// Opens a USB Relay Device using device id
        /// </summary>
        /// <param name="device_ID">Device ID/serial number of device to open</param>
        /// <returns>true if success, false if failed</returns>
        public bool OpenDevice(string device_ID)
        {
            //if device is open, close device
            if (pDevice != IntPtr.Zero)
            {
                CloseDevice();
            }

            //initialize device open flag and relay count and search index
            DeviceOpen = false;
            relay_count = 0;
            var index = -1;

            //verify device available to open
            if (localDeviceList.Count == 0) return false;

            //search for device with matching ID string
            for (var i = 0; i < localDeviceList.Count; i++)
            {
                if (!localDeviceList[i].device_ID.Equals(device_ID)) continue;
                index = i;
                break;
            }

            if (index == -1) //if device not found fire error event
            {
                Error_Handler("Device ID/serial number not found.",
                    "OpenDevice(string)", null, 0);
                return false;
            }

            //open device using device path and set device handle pointer
            pDevice = HID_OpenDdevice(localDeviceList[index].device_path, false);

            //set device open flag
            DeviceOpen = (pDevice != IntPtr.Zero);

            //set device relay count
            if (DeviceOpen)
            {
                relay_count = localDeviceList[index].relay_Count;
                UpdateRelayStatus();
            }
            
            return DeviceOpen;
        }

        /// <summary>
        /// Closes connected USB Relay device
        /// </summary>
        public void CloseDevice()
        {
            if (pDevice != IntPtr.Zero)
            {
                try
                {
                    var result = NativeMethods.CloseHandle(pDevice);
                    if (!result)
                    {
                        var ec = NativeMethods.GetLastError();
                        Error_Handler("Error Closing Device", "NativeMethods.CloseHandle", null, ec);
                    }
                }
                catch (Exception ex)
                {
                    Error_Handler("Exception", "CloseDevice()", ex, 0);
                }

                wait_for_completion = false;
            }

            RelayStatus = new bool[8];
            pDevice = IntPtr.Zero;
            DeviceOpen = false;
        }

        /// <summary>
        /// Turns on all relays
        /// </summary>
        /// <returns>true if success, false if failed</returns>
        public bool ALLRelaysON()
        {
            return SetRelayState(ON, 0);
        }

        /// <summary>
        /// Turns off all relays
        /// </summary>
        /// <returns></returns>
        public bool ALLRelaysOFF()
        {
            return SetRelayState(OFF, 0);
        }

        /// <summary>
        /// Turns on a single relay
        /// </summary>
        /// <param name="relay">relay number</param>
        /// <returns>true if success, false if failed</returns>
        public bool RelayON(int relay)
        {
            return SetRelayState(ON, relay);
        }

        /// <summary>
        /// Turns off a single relay
        /// </summary>
        /// <param name="relay">relay number</param>
        /// <returns>true if success, false if failed</returns>
        public bool RelayOFF(int relay)
        {
            return SetRelayState(OFF, relay);
        }

        /// <summary>
        /// Sets the state of a relay
        /// </summary>
        /// <param name="state">true = On, false = Off</param>
        /// <param name="relay">relay number</param>
        /// <returns>true if success, false if failed</returns>
        public bool SetRelay(bool state, int relay)
        {
            return SetRelayState(state, relay);
        }

        /// <summary>
        /// Sets the Device ID/serial number
        /// </summary>
        /// <param name="deviceID">value to set, must be 5 characters in length</param>
        /// <returns>true if success, false if failed</returns>
        public bool SetDeviceID(string deviceID)
        {
            // check that a device is open
            if (pDevice == IntPtr.Zero | !DeviceOpen)
            {
                Error_Handler("Device is not connected/open.", "SetDeviceID()", null, 0);
                return false;
            }

            //check id string length (must be 5 or greater, only first five char will be used)
            if (deviceID.Length < 5)
            {
                Error_Handler("Device ID must be 5 characters in length.", "SetDeviceID()", null, 0);
                return false;
            }

            try
            {
                // convert deviceID string to char array
                var id = deviceID.ToCharArray(0, 5);

                // define report buffer values
                var rpt_buffer = new byte[] { 0x00, 0xFA, (byte)id[0], (byte)id[1], (byte)id[2], (byte)id[3], (byte)id[4], 0x00, 0x00 };

                // send report buffer to device to change ID
                return HID_Feature(SET, ref rpt_buffer);
            }
            catch (Exception ex)
            {
                Error_Handler("Exception ", "SetDeviceID()", ex, 0);
            }

            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generic method for finding HID devices using Vendor ID and Product ID values, 
        /// if 0x00 is passed for VID and PID values, all paths to HID devices will be returned
        /// </summary>
        /// <param name="VID">Vendor ID</param>
        /// <param name="PID">Product ID</param>
        /// <returns>returns a string array of paths to devices with matching VID and PID values</returns>
        private string[] HID_GetDevicePaths(ushort VID, ushort PID)
        {
            // initialize device path list
            var DevicePathList = new List<string>(); //return List

            // pointer declarations
            var pDeviceInfoSet = new IntPtr(); //pointer to device information set
            var pHandle = new IntPtr(); //pointer to device handle

            // structure declarations
            var devInterfaceDetailData = new NativeMethods.SP_DEVICE_INTERFACE_DETAIL_DATA(); // device detail data, contains data path
            var devInterfaceData = new NativeMethods.SP_DEVICE_INTERFACE_DATA(); //required for setup api calls
            var deviceAttributes = new NativeMethods.HIDD_ATTRIBUTES(); //HID device attributes

            // Set the size parameter for the structures
            devInterfaceDetailData.cbSize = (uint)(IntPtr.Size == sizeof(int)? 
                IntPtr.Size + Marshal.SystemDefaultCharSize : IntPtr.Size);
            devInterfaceData.Size = (uint)Marshal.SizeOf(typeof(NativeMethods.SP_DEVICE_INTERFACE_DATA));
            deviceAttributes.Size = (uint)Marshal.SizeOf(typeof(NativeMethods.HIDD_ATTRIBUTES));
            
            // loop variable declaration
            uint memberIndex = 0; //device enumeration interface indexer

            try
            {
                // Method reference Microsoft document page "Finding and Opening a HID Collection"
                // https://learn.microsoft.com/en-us/windows-hardware/drivers/hid/finding-and-opening-a-hid-collection

                // Get HID GUID
                Guid HIDguid;
                NativeMethods.HidD_GetHidGuid(out HIDguid);

                // Get a pointer to a device information set of all HID devices  
                pDeviceInfoSet = NativeMethods.SetupDiGetClassDevs(
                    ref HIDguid,
                    null,
                    IntPtr.Zero,
                    NativeMethods.DIGCF_DEVICEINTERFACE | NativeMethods.DIGCF_PRESENT);

                if (pDeviceInfoSet == IntPtr.Zero) //if fails, end device search
                {
                    var ec = NativeMethods.GetLastError();
                    Error_Handler("Error getting pointer to device information set.",
                        "NativeMethods.SetupDiGetClassDevs()", null, ec);
                    return new string[0];
                }

                while (true) // Loop through all found HID devices and find all Devices matching the defined pid and vid values
                {
                    try
                    {
                        // get device interface data from the device information set at member index
                        var result = NativeMethods.SetupDiEnumDeviceInterfaces(
                            pDeviceInfoSet,
                            IntPtr.Zero,
                            ref HIDguid,
                            memberIndex,
                            ref devInterfaceData);

                        if (!result) // if fails stop device enumeration, occurs when there are no more HID devices
                        {
                            // check that error code = ERROR_NO_MORE_ITEMS (0x103), if not throw error
                            var error_code = NativeMethods.GetLastError();
                            if (!error_code.Equals(0x103))
                            {
                                Error_Handler("Error getting device interface.",
                                    "NativeMethods.SetupDiEnumDeviceInterfaces()", null, error_code);
                            }
                                
                            break;
                        }

                        // advance device enumeration index for next iteration
                        memberIndex++;

                        // get path to device (found in SP_DEVICE_INTERFACE_DETAIL_DATA structure)
                        result = NativeMethods.SetupDiGetDeviceInterfaceDetail(
                            pDeviceInfoSet,
                            ref devInterfaceData,
                            ref devInterfaceDetailData,
                            NativeMethods.DEFAULT_SIZE,
                            0,
                            IntPtr.Zero);

                        // if failed to get device path, go to next device
                        if (!result | string.IsNullOrEmpty(devInterfaceDetailData.DevicePath)) continue;

                        // open device, returns a handle to opened device
                        pHandle = HID_OpenDdevice(devInterfaceDetailData.DevicePath, true);

                        if (pHandle == IntPtr.Zero) continue;  //if fails, go to next device

                        // If PID and VID are both set to 0, skip VID/PID validation and add device path to list
                        if (!(PID == 0x00 & VID == 0x00))
                        {
                            // Get the device attributes to be used for device validation
                            result = NativeMethods.HidD_GetAttributes(pHandle, ref deviceAttributes);

                            if (!result) continue;  //if fails, go to next device

                            // check device matches defined vid and pid values, if no-match go to next device
                            if (deviceAttributes.ProductID != PID | deviceAttributes.VendorID != VID) continue;
                        }
                        
                        // device found, update device path list
                        DevicePathList.Add(devInterfaceDetailData.DevicePath);
                    }
                    catch (Exception ex)
                    {
                        Error_Handler("Exception ", "HID_GetDevicePaths() - internal loop", ex, 0);
                    }
                    finally
                    {
                        // free resources and reset pointers for next iteration
                        if (pHandle != IntPtr.Zero)
                        {
                            NativeMethods.CloseHandle(pHandle);
                            pHandle = IntPtr.Zero;
                        }
                    }
                } // while loop
            }
            catch (Exception ex)
            {
                Error_Handler("Exception ", "HID_GetDevicePaths()", ex, 0);
            }
            finally
            {
                //free resource
                if (pDeviceInfoSet != IntPtr.Zero) NativeMethods.SetupDiDestroyDeviceInfoList(pDeviceInfoSet);
            }

            return DevicePathList.Count > 0 ? DevicePathList.ToArray() : new string[0];
        }

        /// <summary>
        /// Generic method for Opening and getting the handle to a HID device.
        /// </summary>
        /// <param name="devicePath">path to HID device</param>
        /// <param name="suppressError">true to suppress exception reporting</param>
        /// <returns>on success returns handle to device, if fails returns 0</returns>
        private IntPtr HID_OpenDdevice(string devicePath, bool suppressError)
        {
            try
            {
                var pHandle = NativeMethods.CreateFile(
                    devicePath,
                    NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
                    NativeMethods.FILE_SHARE_READ | NativeMethods.FILE_SHARE_WRITE,
                    IntPtr.Zero,
                    NativeMethods.OPEN_EXISTING,
                    0,
                    IntPtr.Zero);

                if (pHandle != IntPtr.Zero) return pHandle;
                if (!suppressError)
                {
                    var ec = NativeMethods.GetLastError();
                    Error_Handler("Error opening device.", "NativeMethods.CreateFile()", null, ec);
                }
            }
            catch (Exception ex)
            {
                DeviceOpen = false;
                if (!suppressError) Error_Handler("Exception", "OpenHIDdevice()", ex, 0);
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// Generic method that provides direct communication to an opened HID device for setting and getting feature reports
        /// </summary>
        /// <param name="command_type">0 = SET feature, 1= GET feature</param>
        /// <param name="report_buffer">byte array, 9 elements in length</param>
        /// <returns>true if success, false if failed</returns>
        private bool HID_Feature(int command_type, ref byte[] report_buffer)
        {
            if (pDevice == IntPtr.Zero | report_buffer.Length != 9) return false;

            while (wait_for_completion) { }

            wait_for_completion = true;
            var result = false;

            try
            {
                switch (command_type)
                {
                    case SET:
                        result = NativeMethods.HidD_SetFeature(pDevice, report_buffer, report_buffer.Length);
                        if (!result)
                        {
                            var ec = NativeMethods.GetLastError();
                            Error_Handler("Error seiing Feature Report",
                                "NativeMethods.HidD_SetFeature()", null, ec);
                        }
                        break;
                    case GET:
                        result = NativeMethods.HidD_GetFeature(pDevice, report_buffer, report_buffer.Length);
                        if (!result)
                        {
                            var ec = NativeMethods.GetLastError();
                            Error_Handler("Error getting Feature Report",
                                "NativeMethods.HidD_GetFeature()", null, ec);
                        }
                        break;
                }
            }
            catch (Exception ex) 
            {
                Error_Handler("Exception", "HID_FeatureReport() - ", ex, 0);
            }

            wait_for_completion = false;
            return result;
        }

        /// <summary>
        /// Method called by all methods requesting to change relay states
        /// </summary>
        /// <param name="state">true to turn relay(s) on, false to turn the relay(s) off</param>
        /// <param name="relay_num">index of relay to turn on (0 = All relays, otherwise value = relay number)</param>
        /// <returns>true if success</returns>
        private bool SetRelayState(bool state, int relay_num)
        {
            // check that a device is open
            if (pDevice == IntPtr.Zero | !DeviceOpen)
            {
                Error_Handler("Device is not connected/open.",
                    "SetRelayState()", null, 0);
                return false;
            }

            // check that the relay number is valid
            if (relay_num > relay_count)
            {
                Error_Handler("Relay number exceeds relay count of device.",
                    "SetRelayState()", null, 0);
                return false;
            }

            // initialize arrays
            var rpt_buffer = new byte[9];
            var validate = new bool[8];

            // set values for report buffer and validation arrays
            if (relay_num == 0) // All Relays
            {                
                if (state)
                {
                    rpt_buffer[1] = 0xFE;
                    for (var i = 0; i < relay_count; i++)
                    {
                        validate[i] = ON;
                    }
                }
                else
                {
                    rpt_buffer[1] = 0xFC;
                }
            }
            else // Individual Relay
            {
                if (state)
                {
                    rpt_buffer[1] = 0xFF;
                }
                else
                {
                    rpt_buffer[1] = 0xFD;
                }

                Array.Copy(RelayStatus,validate,8);
                validate[relay_num - 1] = state;
            }
            rpt_buffer[2] = (byte)relay_num;

            // check if relay states are already set
            var result = validate.SequenceEqual(RelayStatus);
            if (result) return true;

            // send command buffer to device
            try
            {
                result = HID_Feature(SET, ref rpt_buffer);

                if (!result) return false;

                // check relay states set correctly
                UpdateRelayStatus();
                result = validate.SequenceEqual(RelayStatus);
                if (result) return true;

                Error_Handler("Error validating relay states.",
                    "SetRelayState()", null, 0);

                return false;
            }
            catch (Exception ex)
            {
                Error_Handler("Exception", "SetRelayState() - ", ex, 0);
                return false;
            }          
        }

        /// <summary>
        /// Updates the RelayStatus property with the current state of all relays
        /// </summary>
        private void UpdateRelayStatus()
        {
            // check that a device is open
            if (pDevice == IntPtr.Zero | !DeviceOpen)
            {
                Error_Handler("Device is not connected/open.", "GetRelayStatus()", null, 0);
                RelayStatus = new bool[8];
                return;
            }

            // initialize report buffer
            var rpt_buffer = new byte[9];

            try
            {
                // send get data command to device
                var result = HID_Feature(GET, ref rpt_buffer);

                if (!result)
                {
                    RelayStatus = new bool[8];
                    return;
                }

                // create bit array from relay status byte value
                var bitarry = new BitArray(new[] { rpt_buffer[8] });

                // Update RelayStatus property from bit array
                bitarry.CopyTo(RelayStatus, 0);

                return;
            }
            catch (Exception ex)
            {
                Error_Handler("Exception", "GetRelayStates() - ", ex, 0);
            }

            RelayStatus = new bool[8];
        }

        #endregion

        #region Native API call Definitions

        /// <summary>
        /// Class provides function prototypes and strcture definitions for native API calls
        /// </summary>
        internal static class NativeMethods
        {
            #region Constants and Flags

            public const uint DIGCF_PRESENT = 0x02; // devices that are currently present in a system
            public const uint DIGCF_DEVICEINTERFACE = 0x10; // devices that support device interfaces
            public const uint DEFAULT_SIZE = 0x0FFF;
            public const uint GENERIC_READ = 0x80000000; // Read access
            public const uint GENERIC_WRITE = 0x40000000; // Write access
            public const uint FILE_SHARE_READ = 0x00000001; // Enables subsequent open operations on a file or device to request read access.
            public const uint FILE_SHARE_WRITE = 0x00000002; // Enables subsequent open operations on a file or device to request write access.
            public const uint OPEN_EXISTING = 3; // Opens a file or device, only if it exists.

            #endregion

            #region Structures

            // reference: https://learn.microsoft.com/en-us/windows/win32/api/setupapi/ns-setupapi-sp_device_interface_data
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct SP_DEVICE_INTERFACE_DATA
            {
                public uint Size;
                private readonly Guid InterfaceClassGuid;
                private readonly int Flags;
                private readonly IntPtr Reserved;
            }

            // reference: https://learn.microsoft.com/en-us/windows/win32/api/setupapi/ns-setupapi-sp_device_interface_detail_data_a
            [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
            public struct SP_DEVICE_INTERFACE_DETAIL_DATA
            {
                public uint cbSize;

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int) DEFAULT_SIZE)]
                public readonly string DevicePath;
            }

            // reference: https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/ns-hidsdi-_hidd_attributes
            [StructLayout(LayoutKind.Sequential)]
            public struct HIDD_ATTRIBUTES
            {
                public uint Size;
                public readonly ushort VendorID;
                public readonly ushort ProductID;
                private readonly ushort VersionNumber;
            }

            #endregion

            #region Native function prototypes          

            /// <summary>
            /// Function returns a handle to a device information set that contains requested device information elements.
            /// </summary>
            /// <param name="classGuid">A pointer to the GUID for a device setup class or a device interface class.</param>
            /// <param name="enumerator">Specification string, optional and can be null</param>
            /// <param name="hwndParent">Handle for a user interface device instance. Optional and can be zero.</param>
            /// <param name="Flags">Specifies control options that filter the device information elements.</param>
            /// <returns>returns a handle to a device information set</returns>
            /// ref: https://learn.microsoft.com/en-us/windows/win32/api/setupapi/nf-setupapi-setupdigetclassdevsw
            [DllImport("setupapi.dll", SetLastError = true)]
            public static extern IntPtr SetupDiGetClassDevs(
                ref Guid classGuid,
                [MarshalAs(UnmanagedType.LPStr)] string enumerator,
                IntPtr hwndParent,
                uint Flags);

            /// <summary>
            /// Function enumerates the device interfaces that are contained in a device information set.
            /// </summary>
            /// <param name="DeviceInfoSet">pointer to a device information set that contains the device interfaces</param>
            /// <param name="DeviceInfoData">pointer to an SP_DEVINFO_DATA structure. Optional can be set to zero</param>
            /// <param name="InterfaceClassGuid">pointer to a GUID that specifies the device interface class.</param>
            /// <param name="MemberIndex">A zero-based index into the list of interfaces in the device information set.</param>
            /// <param name="DeviceInterfaceData">On successful return, a completed SP_DEVICE_INTERFACE_DATA structure</param>
            /// <returns>returns TRUE if the function completed without error.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows/win32/api/setupapi/nf-setupapi-setupdienumdeviceinterfaces
            [DllImport("setupapi.dll", SetLastError = true)]
            public static extern bool SetupDiEnumDeviceInterfaces(
                IntPtr DeviceInfoSet,
                IntPtr DeviceInfoData, 
                ref Guid InterfaceClassGuid,
                uint MemberIndex,
                ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

            /// <summary>
            /// Function returns details about a device interface.
            /// </summary>
            /// <param name="DeviceInfoSet">pointer to the device information set, typically returned by SetupDiEnumDeviceInterfaces.</param>
            /// <param name="DeviceInterfaceData">reference to a SP_DEVICE_INTERFACE_DATA structure</param>
            /// <param name="DeviceInterfaceDetailData">reference to SP_DEVICE_INTERFACE_DETAIL_DATA structure.</param>
            /// <param name="DeviceInterfaceDetailDataSize">Size of DeviceInterfaceDetailData buffer.</param>
            /// <param name="RequiredSize">variable that receives the required size of the DeviceInterfaceDetailData buffer.</param>
            /// <param name="DeviceInfoData">pointer to a buffer that receives information about the device, optional can be null</param>
            /// <returns>Ignore return value.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows/win32/api/setupapi/nf-setupapi-setupdigetdeviceinterfacedetaila
            [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetupDiGetDeviceInterfaceDetail(
                IntPtr DeviceInfoSet,
                ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
                ref SP_DEVICE_INTERFACE_DETAIL_DATA DeviceInterfaceDetailData,
                uint DeviceInterfaceDetailDataSize,
                uint RequiredSize,
                IntPtr DeviceInfoData);

            /// <summary>
            /// Function deletes a device information set and frees all associated memory.
            /// </summary>
            /// <param name="DeviceInfoSet">A handle to the device information set to delete.</param>
            /// <returns>returns TRUE if it is successful.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows/win32/api/setupapi/nf-setupapi-setupdidestroydeviceinfolist
            [DllImport("setupapi.dll", SetLastError = true)]
            public static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

            /// <summary>
            /// Creates or opens a file or I/O device.
            /// </summary>
            /// <param name="lpFileName">The name of the file or path to a device to be created or opened.</param>
            /// <param name="dwDesiredAccess">The requested access to the file or devic</param>
            /// <param name="dwShareMode">The requested sharing mode of the file or device</param>
            /// <param name="lpSecurityAttributes">pointer to a SECURITY_ATTRIBUTES structure. Optional can be zero</param>
            /// <param name="dwCreationDisposition">action to take on a file or device that exists or does not exist.</param>
            /// <param name="dwFlagsAndAttributes"> file or device attributes and flags</param>
            /// <param name="hTemplateFile">handle to a template file with the GENERIC_READ access right.</param>
            /// <returns>If the function succeeds, the returns an open handle to the specified file, device.</returns>
            /// ref:  https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilea
            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr CreateFile(
                string lpFileName,
                uint dwDesiredAccess,
                uint dwShareMode,
                IntPtr lpSecurityAttributes,
                uint dwCreationDisposition,
                uint dwFlagsAndAttributes,
                IntPtr hTemplateFile);

            /// <summary>
            /// Closes an open object handle.
            /// </summary>
            /// <param name="handle">A valid handle to an open object.</param>
            /// <returns>returns TRUE if it is successful.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows/win32/api/handleapi/nf-handleapi-closehandle
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool CloseHandle(IntPtr handle);

            /// <summary>
            /// Retrieves the calling thread's last-error code value.
            /// </summary>
            /// <returns>The return value is the calling thread's last-error code.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows/win32/api/errhandlingapi/nf-errhandlingapi-getlasterror
            [DllImport("kernel32.dll")]
            public static extern uint GetLastError();

            /// <summary>
            /// Routine returns the device interface GUID for HIDClass devices.
            /// </summary>
            /// <param name="HidGuid">Pointer the routine uses to return the device interface GUID for HIDClass devices.</param>
            /// ref: https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_gethidguid
            [DllImport("hid.dll", SetLastError = true)]
            public static extern void HidD_GetHidGuid(out Guid HidGuid);

            /// <summary>
            /// Routine returns the attributes of a specified top-level collection.
            /// </summary>
            /// <param name="HidDeviceObject">Specifies an open handle to a top-level collection.</param>
            /// <param name="Attributes">Pointer to a caller-allocated HIDD_ATTRIBUTES structure</param>
            /// <returns>returns TRUE if succeeds; otherwise, it returns FALSE.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_getattributes
            [DllImport("hid.dll", SetLastError = true)]
            public static extern bool HidD_GetAttributes(
                IntPtr HidDeviceObject, 
                ref HIDD_ATTRIBUTES Attributes);

            /// <summary>
            /// Routine returns the embedded string of a top-level collection that identifies the manufacturer's product.
            /// </summary>
            /// <param name="HidDeviceObject">open handle to a top-level collection.</param>
            /// <param name="Buffer">Pointer to a caller-allocated buffer that the routine uses to return the product string.</param>
            /// <param name="BufferLength">Specifies the length, in bytes, of a caller-allocated buffer.</param>
            /// <returns>returns TRUE if succeeds; otherwise, it returns FALSE.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_getproductstring
            [DllImport("hid.dll", SetLastError = true)]
            public static extern bool HidD_GetProductString(
                IntPtr HidDeviceObject, 
                IntPtr Buffer, 
                uint BufferLength);

            /// <summary>
            /// Routine returns a feature report from a specified top-level collection.
            /// </summary>
            /// <param name="HidDeviceObject">An open handle to a top-level collection.</param>
            /// <param name="ReportBuffer">caller-allocated HID report buffer to return the specified feature report.</param>
            /// <param name="ReportBufferLength">The size of the report buffer in bytes.</param>
            /// <returns>returns TRUE if succeeds; otherwise, it returns FALSE.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_getfeature
            [DllImport("hid.dll", SetLastError = true)]
            public static extern bool HidD_GetFeature(
                IntPtr HidDeviceObject, 
                byte[] ReportBuffer, 
                int ReportBufferLength);

            /// <summary>
            /// Routine sends a feature report to a top-level collection.
            /// </summary>
            /// <param name="HidDeviceObject">An open handle to a top-level collection.</param>
            /// <param name="ReportBuffer">caller-allocated feature report buffer that the caller uses to specify a HID report.</param>
            /// <param name="ReportBufferLength">The size of the report buffer in bytes.</param>
            /// <returns>returns TRUE if succeeds; otherwise, it returns FALSE.</returns>
            /// ref: https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_setfeature
            [DllImport("hid.dll", SetLastError = true)]
            public static extern bool HidD_SetFeature(
                IntPtr HidDeviceObject, 
                byte[] ReportBuffer, 
                int ReportBufferLength);

            #endregion
        }

        #endregion

    }
}
