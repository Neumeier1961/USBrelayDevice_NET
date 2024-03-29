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

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using HIDusbRelay_Test.Properties;
using USBrelayDeviceNET;

namespace HIDusbRelay_Test
{
    public partial class UI_Main : Form
    {
        #region Member Declarations

        private readonly Settings mus = Settings.Default;

        private USBrelayDevice usbRelay;

        private USBrelayDevice.DeviceInfo[] deviceInfo;

        private USBrelayDevice.DeviceInfo current_device;


        private static string AssemblyVersion
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                return attributes.Length == 0 ? "Unknown" : ((AssemblyFileVersionAttribute)attributes[0]).Version;
            }
        }

        #endregion

        #region Form and Common Methods

        public UI_Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //update UI with settings values
            rb_Default.Checked = mus.generic;
            rb_DeviceID.Checked = mus.Dev_ID;
            rb_FirstDevice.Checked = mus.First_Dev;
            rb_DoNotOpen.Checked = mus.DoNotOpen;
            tb_DeviceID.Text = mus.device_ID;

            lb_ConnectedDevice.Text = "Open Device: NONE";

            

            //example usage of USBrelayDevice class initialization overloads
            if (rb_DeviceID.Checked)
            {
                //typically used whn only a device having a specific ID should be opened for this class
                usbRelay = new USBrelayDevice(mus.device_ID);
                usbRelay.USBrelayError += USBrelayError; //hook up error handler

                //fill UI controls
                GetDevices(false);
                if (deviceInfo.Length > 0)
                {
                    for (int i = 0; i < deviceInfo.Length; i++)
                    {
                        if (deviceInfo[i].device_ID.Equals(mus.device_ID))
                        {
                            cb_Devices.SelectedIndex = i + 1;
                            current_device = deviceInfo[i];
                            lb_ConnectedDevice.Text = "Open Device: [" + current_device.index + "] " + cb_Devices.SelectedItem;
                            tb_DeviceID.Text = current_device.device_ID;
                            UpdateRelayStatus();
                            break;
                        }
                    }
                }
            }
            else if (rb_FirstDevice.Checked)
            {
                //typically used when only one device is attached to the system and should be opened when class instance is created
                usbRelay = new USBrelayDevice(true);
                usbRelay.USBrelayError += USBrelayError; //hook up error handler

                //fill UI controls
                GetDevices(false);
                if (usbRelay.DeviceOpen & deviceInfo.Length > 0)
                {
                    cb_Devices.SelectedIndex = 1;
                    current_device = deviceInfo[0];
                    lb_ConnectedDevice.Text = "Open Device: [" + current_device.index + "] " + cb_Devices.SelectedItem;
                    tb_DeviceID.Text = current_device.device_ID;
                    UpdateRelayStatus();
                }
            }
            else if (rb_DoNotOpen.Checked)
            {
                //typically used when only one device is attached to the system and should not be opened when class instance is created
                usbRelay = new USBrelayDevice(false);
                usbRelay.USBrelayError += USBrelayError; //hook up error handler

                //fill UI controls
                GetDevices(false);
            }
            else
            {
                //typically used when multiple devices are attached to the system and user should select a device
                usbRelay = new USBrelayDevice();
                usbRelay.USBrelayError += USBrelayError; //hook up error handler

                //fill UI controls
                GetDevices(false);
            }

            var ok = ExceptionLogging.CheckLogFileSize();
            if (!ok)
            {
                MessageBox.Show("Error Log has exceeded maximum size limit." +
                                "\nReview and clear Error Log file.");
            }

            Text = Text + "   ver. " + AssemblyVersion;

            if (usbRelay.DeviceOpen) return;

            Label[] rly = { RLY_1, RLY_2, RLY_3, RLY_4, RLY_5, RLY_6, RLY_7, RLY_8 };
            for (var i = 0; i < 8; i++)
            {
                rly[i].ForeColor = Color.White;
                rly[i].BackColor = Color.LightGray;
                rly[i].Enabled = false;
            }
        }

        private void USBrelayError(object sender, USBrelayDevice.USBrelayEventArgs e)
        {
            if (e.IsException)
            {
                MessageBox.Show("Exception in " + e.Source + "\n" + e.Error);
                ExceptionLogging.Log(e.ExceptionThrown, "Serial Number: " + current_device.device_ID);
            }
            else
            {
                var str = "Error in " + e.Source + "\n" + e.Error;
                MessageBox.Show(str);
                ExceptionLogging.Log(str + "\nSerial Number: " + current_device.device_ID);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!usbRelay.IsDisposed) usbRelay.Dispose();
            mus.generic = rb_Default.Checked;
            mus.Dev_ID = rb_DeviceID.Checked;
            mus.First_Dev = rb_FirstDevice.Checked;
            mus.DoNotOpen = rb_DoNotOpen.Checked;
            mus.Save();
        }

        private void GetDevices(bool closeOpenDevice)
        {
            if(usbRelay.DeviceOpen & closeOpenDevice) btn_CloseDevice_Click(null,null);
            cb_Devices.Items.Clear();

            deviceInfo = usbRelay.GetUSBrelayDevices(closeOpenDevice).ToArray();

            if (deviceInfo.Length > 0 & usbRelay.DevicesFound)
            {
                cb_Devices.Items.Add("Select Device");
                foreach (var _deviceInfo in deviceInfo)
                {
                    cb_Devices.Items.Add(_deviceInfo.product_name + " : " + _deviceInfo.device_ID);
                }

                cb_Devices.SelectedIndex = 0;

                UpdateRelayStatus();
            }
            else
            {
                cb_Devices.Items.Add("No Devices Found");
                cb_Devices.SelectedIndex = 0;
            }
        }

        private void UpdateRelayStatus()
        {
            if (!usbRelay.DeviceOpen | cb_Devices.SelectedIndex.Equals(0)) return;
            Label[] rly = { RLY_1, RLY_2, RLY_3, RLY_4, RLY_5, RLY_6, RLY_7, RLY_8 };

            var cnt = deviceInfo[cb_Devices.SelectedIndex - 1].relay_Count;

            for (var i = 0; i < usbRelay.RelayStatus.Length; i++)
            {
                if (i < cnt)
                {
                    rly[i].Enabled = true;
                    rly[i].ForeColor = Color.Black;
                    rly[i].BackColor = usbRelay.RelayStatus[i] ? Color.Lime : Color.LightYellow;
                }
                else
                {
                    rly[i].ForeColor = Color.White;
                    rly[i].BackColor = Color.LightGray;
                    rly[i].Enabled = false;
                }
            }
        }

        #endregion

        #region UI Events

        private void btn_DeviceList_Click(object sender, EventArgs e)
        {
            GetDevices(true);
        }

        private void btn_OpenDevice_Click(object sender, EventArgs e)
        {
            if (cb_Devices.SelectedIndex.Equals(0)) return;

            if(usbRelay.DeviceOpen) usbRelay.CloseDevice();

            bool res;

            if (deviceInfo.Length < 2) //if only one device, open device
            {

                if(!chk_UseDeviceID.Checked) res = usbRelay.OpenDevice(0);
                else res = usbRelay.OpenDevice(deviceInfo[cb_Devices.SelectedIndex - 1].device_ID);

                if (!res)
                {
                    lb_ConnectedDevice.Text = "Open Device: NONE";
                    lb_DeviceInfo.Text = "Path: NA";
                    MessageBox.Show("Error opening device.");
                }
                else
                {
                    current_device = deviceInfo[cb_Devices.SelectedIndex - 1];
                    lb_ConnectedDevice.Text = "Open Device: [" + current_device.index + "] " + cb_Devices.SelectedItem;
                    tb_DeviceID.Text = current_device.device_ID;
                    mus.device_ID = current_device.device_ID;
                    mus.Save();
                    UpdateRelayStatus();
                }
            }
            else
            {
                if (!chk_UseDeviceID.Checked) res = usbRelay.OpenDevice(cb_Devices.SelectedIndex - 1);
                else res = usbRelay.OpenDevice(deviceInfo[cb_Devices.SelectedIndex - 1].device_ID);

                if (!res)
                {
                    lb_ConnectedDevice.Text = "Open Device: NONE";
                    lb_DeviceInfo.Text = "Path: NA";
                    MessageBox.Show("Error opening device.");
                }
                else
                {
                    current_device = deviceInfo[cb_Devices.SelectedIndex - 1];
                    lb_ConnectedDevice.Text = "Open Device: [" + current_device.index + "] " + cb_Devices.SelectedItem;
                    tb_DeviceID.Text = current_device.device_ID;
                    mus.device_ID = current_device.device_ID;
                    mus.Save();
                    UpdateRelayStatus();
                }
            }
        }

        private void btn_CloseDevice_Click(object sender, EventArgs e)
        {
            if(!usbRelay.DeviceOpen) return;
            usbRelay.CloseDevice();
            lb_ConnectedDevice.Text = "Open Device: NONE";
            current_device = new USBrelayDevice.DeviceInfo();
            Label[] rly = { RLY_1, RLY_2, RLY_3, RLY_4, RLY_5, RLY_6, RLY_7, RLY_8 };
            for (var i = 0; i < 8; i++)
            {
                rly[i].ForeColor = Color.White;
                rly[i].BackColor = Color.LightGray;
                rly[i].Enabled = false;
            }
        }

        private void RLY_Click(object sender, EventArgs e)
        {
            var x = Convert.ToInt32(((Label)sender).Name.Replace("RLY_", "")) - 1;
            var res = false;
            if (x < usbRelay.RelayStatus.Length)
            {
                res = usbRelay.SetRelay(!usbRelay.RelayStatus[x], x + 1);
            }

            if (!res) MessageBox.Show("Error setting relay state.");
            else UpdateRelayStatus();
        }

        private void btn_RlyOn_Click(object sender, EventArgs e)
        {
            var res = usbRelay.RelayON((int)nud_relay.Value);

            if (!res) MessageBox.Show("Error setting relay state.");
            else UpdateRelayStatus();
        }

        private void btn_RlyOff_Click(object sender, EventArgs e)
        {
            var res = usbRelay.RelayOFF((int)nud_relay.Value);

            if (!res) MessageBox.Show("Error setting relay state.");
            else UpdateRelayStatus();
        }

        private void btn_AllON_Click(object sender, EventArgs e)
        {
            if (!usbRelay.DeviceOpen) return;
            var x = usbRelay.ALLRelaysON();
            if (!x) MessageBox.Show("Error setting relay state.");
            else UpdateRelayStatus();
        }

        private void btn_AllOFF_Click(object sender, EventArgs e)
        {
            if (!usbRelay.DeviceOpen) return;
            var x = usbRelay.ALLRelaysOFF();
            if (!x) MessageBox.Show("Error setting relay state.");
            else UpdateRelayStatus();
        }

        private void cb_Devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Devices.SelectedIndex == 0)
            {
                lb_DeviceInfo.Text = "Device Info:\nNo Device Selected.";
                return;
            }

            lb_DeviceInfo.Text = String.Format("Device Info:\nDevice ID: {0}\nType: {1}\nPath: {2}\nRelay Count: {3}",
                deviceInfo[cb_Devices.SelectedIndex - 1].device_ID,
                deviceInfo[cb_Devices.SelectedIndex - 1].product_name,
                deviceInfo[cb_Devices.SelectedIndex - 1].device_path,
                deviceInfo[cb_Devices.SelectedIndex - 1].relay_Count);
        }

        private void btn_Copy_Click(object sender, EventArgs e)
        {
            if (cb_Devices.Items.Count <= 1) return;

            var info = USBrelayDevice.GetLibraryInfo();
            var str = "Lib: " + info[0] + ", ver: " + info[1] + "\n";
            for (var i = 0; i < deviceInfo.Length; i++)
            {
                str = str + "\nIndex: " + (i + 1)
                      + "\nDevice: " + deviceInfo[i].product_name
                      + "\nID: " + deviceInfo[i].device_ID
                      + "\nPath: " + deviceInfo[i].device_path
                      + "\n";
            }
            Clipboard.SetText(str);
        }

        private void btn_SetID_Click(object sender, EventArgs e)
        {
            var res = usbRelay.SetDeviceID(tb_DeviceID.Text);
            GetDevices(false);
            MessageBox.Show(res ? "Device ID set to new value." : "Failed to set device ID");
        }

        #endregion

        #region Nenu Strip Item Events

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExceptionLogging.SaveErrorLog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExceptionLogging.OpenErrorLog();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExceptionLogging.ClearErrorLog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dllVerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = USBrelayDevice.GetLibraryInfo();
            MessageBox.Show(info[0] + "\nVersion: " + info[1], "DLL Information");
        }

        #endregion

        #region USB Device Insertion/Removal Detection

        private const int WM_DEVICECHANGE = 0x0219; // Windows System Message Constant, Device staus changed
        //private const int DBT_DEVICEREMOVECOMPLETE = 0x8004; // Message Parameter Constant, device has been removed from system
        //private const int DBT_DEVICEARRIVAL = 0x8000; // Message Parameter Constant, device has been added to system
        //private const int WM_NCACTIVATE = 0x0086;
        //private const int WM_INITDIALOG = 0x0110;

        /// <summary>
        /// Event called when there is a Windows Message
        /// </summary>
        /// <param name="m">System Message structure</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg != WM_DEVICECHANGE) return; //Check if message is for a device change

            var cnt_before = deviceInfo.Length;
            var cnt_after = usbRelay.GetUSBrelayDevices(false).ToArray().Length;
            var diff = cnt_after - cnt_before;
            if (diff == 0) // no change to ISB Relay Devices
            {
                return;
            }

            GetDevices(false);
            var found = false;
            if (diff > 0) //device added
            {
                if (usbRelay.DeviceOpen & cnt_after > 1)
                {
                    for (var i = 0; i < cnt_after; i++)
                    {
                        if (!current_device.device_path.Equals(deviceInfo[i].device_path))
                            continue;
                        cb_Devices.SelectedIndex = i + 1;
                        if (i == current_device.index) break;
                        usbRelay.CloseDevice();
                        btn_OpenDevice_Click(null, null);
                        break;
                    }                
                }
                MessageBox.Show("Device Added");
                return;              
            }

            //usb relay device has been removed

            if (!usbRelay.DeviceOpen)
            {
                MessageBox.Show("Device Removed");
                return;
            }

            if (cnt_after == 0)
            {
                MessageBox.Show("Connected Device Removed");
                btn_CloseDevice_Click(null, null);
                return;
            }

            for (var i = 0; i < cnt_after; i++)
            {
                if (!current_device.device_path.Equals(deviceInfo[i].device_path)) 
                    continue;
                cb_Devices.SelectedIndex = i + 1;
                if (!i.Equals(current_device.index))
                {
                    btn_OpenDevice_Click(null, null);
                }
                    
                found = true;
                break;
            }
            if (!found)
            {
                btn_CloseDevice_Click(null, null);
                MessageBox.Show("Connected Device Removed");  
            }
            else MessageBox.Show("Device Removed");
        }

        #endregion

    }
}
