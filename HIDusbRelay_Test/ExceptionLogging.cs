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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace HIDusbRelay_Test
{
    /// <summary>  
    /// Static Exception Logging class. 
    /// Provides static methods for logging exceptions and program generated messages. 
    /// Contains static methods for opening, saving and clearing log file
    /// </summary>  
    public static class ExceptionLogging
    {
        /// <summary>
        /// Checks if the Log file has exceeded the maximum allowable file size.
        /// Prevents improperly handled loop errors from overfilling the file which can cause cascading issues.
        /// In Main UI, recommend calling this method at startup and advising user if file needs to be reviewed and cleared.
        /// </summary>
        /// <returns>trutns false if file size has been exeeded, true if file size is OK.</returns>
        public static bool CheckLogFileSize()
        {
            var assemb = Assembly.GetEntryAssembly();
            if (assemb == null) return false; //error getting Assembly

            var fullName = assemb.Location;
            var appName = Path.GetFileNameWithoutExtension(fullName);
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                            "\\" + appName;
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
                return true;
            }
            var filepath = appFolder + "\\ErrorLog.txt";
            if(!File.Exists(filepath)) return true;

            var fileSize = new FileInfo(filepath).Length;
            return fileSize <= 1500000; //set max allowable file size
        }

        /// <summary>
        /// Logs exceptions to text file stored in Application's ProgramData folder
        /// </summary>
        /// <param name="ex">exception to log</param>
        public static void Log(Exception ex)
        {
            if(!CheckLogFileSize() | ex == null) return;

            var trace = ex.StackTrace;
            var extype = ex.GetType();
            var exMessage = ex.Message;
            var n = Environment.NewLine;
            try
            {
                var appVesion = "UNKNOWN";
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length > 0) appVesion = ((AssemblyFileVersionAttribute)attributes[0]).Version;

                var assemb = Assembly.GetEntryAssembly();
                if (assemb == null) return;
                var fullName = assemb.Location;
                var appName = Path.GetFileNameWithoutExtension(fullName);
                var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                                "\\" + appName;
                var filepath = appFolder + "\\ErrorLog.txt";

                if (!Directory.Exists(appFolder)) Directory.CreateDirectory(appFolder);

                var log =
                    String.Format(
                        "--------------------------------------------------------------------------------------------" + n +
                        "{0}, Version: {1}" + n +
                        "Log Date-Time: {2}" + n +
                        "Exception Type: {3}" + n +
                        "Exception Message : {4}" + n +
                        "Stack Trace : {5}" + n +
                        "------------------------------ End of Log Entry -------------------------------------" + n + n,
                        appName, appVesion, DateTime.Now, extype, exMessage, trace);

                File.AppendAllText(filepath, log);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Logs exceptions to text file stored in Application's ProgramData folder
        /// </summary>
        /// <param name="ex">exception to log</param>
        /// <param name="footer">custom test to add at the end of exception log</param>
        public static void Log(Exception ex, string footer)
        {
            if (!CheckLogFileSize() | ex == null) return;

            var trace = ex.StackTrace;
            var extype = ex.GetType();
            var exMessage = ex.Message;
            var n = Environment.NewLine;
            try
            {
                var appVesion = "UNKNOWN";
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length > 0) appVesion = ((AssemblyFileVersionAttribute)attributes[0]).Version;

                var assemb = Assembly.GetEntryAssembly();
                if (assemb == null) return;
                var fullName = assemb.Location;
                var appName = Path.GetFileNameWithoutExtension(fullName);
                var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                                    "\\" + appName;
                var filepath = appFolder + "\\ErrorLog.txt";

                if (!Directory.Exists(appFolder)) Directory.CreateDirectory(appFolder);

                var log =
                    String.Format(
                        "--------------------------------------------------------------------------------------------" + n +
                        "{0}, Version: {1}" + n +
                        "Log Date-Time: {2}" + n +
                        "Exception Type: {3}" + n +
                        "Exception Message : {4}" + n +
                        "Stack Trace : {5}" + n +
                        "{6}" + n +
                        "------------------------------ End of Log Entry -------------------------------------" + n + n,
                        appName, appVesion, DateTime.Now, extype, exMessage, trace, footer);

                File.AppendAllText(filepath, log);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Logs program message to text file stored in Application's ProgramData folder
        /// </summary>
        /// <param name="message">message to pass to log</param>
        public static void Log(string message)
        {
            if (!CheckLogFileSize()) return;

            var n = Environment.NewLine;
            try
            {
                var appVesion = "UNKNOWN";
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length > 0) appVesion = ((AssemblyFileVersionAttribute)attributes[0]).Version;

                var assemb = Assembly.GetEntryAssembly();
                if (assemb == null) return;
                var fullName = assemb.Location;
                var appName = Path.GetFileNameWithoutExtension(fullName);
                var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                                    "\\" + appName;
                var filepath = appFolder + "\\ErrorLog.txt";

                if (!Directory.Exists(appFolder)) Directory.CreateDirectory(appFolder);

                var log =
                    String.Format(
                        "--------------------------------------------------------------------------------------------" + n +
                        "{0}, Version: {1}" + n +
                        "Log Date-Time: {2}" + n,
                        appName, appVesion, DateTime.Now);

                log = log + "Message: " + message + n +
                        "------------------------------ End of Log Entry -------------------------------------" + n + n;

                File.AppendAllText(filepath, log);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Opens Error Log in default viewer.
        /// If no log file is found, creates new log file.
        /// </summary>
        public static void OpenErrorLog()
        {
            var assemb = Assembly.GetEntryAssembly();
            if (assemb == null)
            {
                MessageBox.Show("Exception Logging\nENtry Assembly Not FOund.",
                    "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var fullName = assemb.Location;
            var appName = Path.GetFileNameWithoutExtension(fullName);
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + appName;
            var filepath = appFolder + "\\ErrorLog.txt";

            if (File.Exists(filepath))
            {
                try
                {
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening log file.\nex: " + ex, "Error Log",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var n = Environment.NewLine;
                try
                {
                    File.WriteAllText(filepath, "log creted on: " + DateTime.Now + n + n);
                }
                catch (Exception) { }

                MessageBox.Show("No Error Log file found.\nNew log file has been created.",
                           "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Saves Error Log to user selected location.
        /// If no log file is found, creates new log file.
        /// </summary>
        public static void SaveErrorLog()
        {
            var assemb = Assembly.GetEntryAssembly();
            if (assemb == null)
            {
                MessageBox.Show("Exception Logging\nENtry Assembly Not FOund.",
                    "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var fullName = assemb.Location;
            var appName = Path.GetFileNameWithoutExtension(fullName);
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + appName;
            var filepath = appFolder + "\\ErrorLog.txt";

            if (File.Exists(filepath))
            {
                try
                {
                    using (var sfd = new SaveFileDialog())
                    {
                        sfd.FileName = "ErrorLog.txt";
                        sfd.DefaultExt = "txt";
                        sfd.AddExtension = true;
                        var res = sfd.ShowDialog();
                        if (res != DialogResult.OK) return;

                        var txt = File.ReadAllLines(filepath);
                        File.WriteAllLines(sfd.FileName, txt);

                        const string msg = "Error log saved.\nwould you like to open the file?";
                        var dlg = MessageBox.Show(msg, "Error Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlg != DialogResult.Yes) return;
                        if (File.Exists(sfd.FileName)) Process.Start(sfd.FileName);
                        else MessageBox.Show("File not found.",
                           "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving log file.\nex: " + ex, "File Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var n = Environment.NewLine;
                try
                {
                    File.WriteAllText(filepath, "log creted on: " + DateTime.Now + n + n);
                }
                catch (Exception) { }

                MessageBox.Show("No Error Log file found.\nNew log file has been created.",
                           "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Deletes all contens of Error log.
        /// If no log file is found, creates new log file.
        /// </summary>
        public static void ClearErrorLog()
        {
            var dlg = MessageBox.Show("All contenets of the Error Log will be deleted.\n" +
                                      "Confirm you want to proceed", "Confirmation Required",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dlg == DialogResult.Cancel) return;

            var assemb = Assembly.GetEntryAssembly();
            if (assemb == null)
            {
                MessageBox.Show("Exception Logging\nENtry Assembly Not FOund.",
                    "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var fullName = assemb.Location;
            var appName = Path.GetFileNameWithoutExtension(fullName);
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + appName;
            var filepath = appFolder + "\\ErrorLog.txt";
            var n = Environment.NewLine;

            try
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                    File.WriteAllText(filepath, "log cleared on: " + DateTime.Now + n + n);
                    MessageBox.Show("Log file has been cleared.", "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        File.WriteAllText(filepath, "log creted on: " + DateTime.Now + n + n);
                    }
                    catch (Exception) { }

                    MessageBox.Show("No Error Log file found.\nNew log file has been created.",
                        "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing log.\nex: " + ex, "File Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
