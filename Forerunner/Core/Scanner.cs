/*
==============================================================================
Copyright � Jason Drawdy (CloneMerge)

All rights reserved.

The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Except as contained in this notice, the name of the above copyright holder
shall not be used in advertising or otherwise to promote the sale, use or
other dealings in this Software without prior written authorization.
==============================================================================
*/

#region Imports

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Diagnostics;

#endregion
namespace Forerunner
{
    public class Scanner
    {
        #region Variables

        /// <summary>
        /// The amount of requests to send to a destination address in order to determine a ping response.
        /// </summary>
        public int Probes { get; set; } = 3;
        /// <summary>
        /// The amount of time � in milliseconds � a probe should take before sending another request.
        /// </summary>
        public int Timeout { get; set; } = 1000;
        /// <summary>
        /// A collection of information types that should be retrieved during a scan.
        /// </summary>
        public ScanFetchers Fetchers { get; set; }
        /// <summary>
        /// Occurs when a scan has been completed.
        /// </summary>
        public event ScanCompleteHandler ScanComplete;
        /// <summary>
        /// Occurs when an asynchronous scan has been completed.
        /// </summary>
        public event ScanAsyncCompleteHandler ScanAsyncComplete;
        /// <summary>
        /// Occurs when a range scan has been completed.
        /// </summary>
        public event ScanRangeCompleteHandler ScanRangeComplete;
        /// <summary>
        /// Occurs when an asynchronous range scan has been completed.
        /// </summary>
        public event ScanRangeAsyncCompleteHandler ScanRangeAsyncComplete;
        /// <summary>
        /// Occurs when a list scan has been completed.
        /// </summary>
        public event ScanListCompleteHandler ScanListComplete;
        /// <summary>
        /// Occurs when an asynchronous list scan has been completed.
        /// </summary>
        public event ScanListAsyncCompleteHandler ScanListAsyncComplete;
        /// <summary>
        /// Occurs when a port knock scan has been completed.
        /// </summary>
        public event PortKnockCompleteHandler PortKnockComplete;
        /// <summary>
        /// Occurs when an asynchronous port knock scan has been completed.
        /// </summary>
        public event PortKnockAsyncCompeleteHandler PortKnockAsyncComplete;
        /// <summary>
        /// Occurs when a range port knock scan has been completed.
        /// </summary>
        public event PortKnockRangeCompleteHandler PortKnockRangeComplete;
        /// <summary>
        /// Occurs when an asynchronous range port knock scan has been completed.
        /// </summary>
        public event PortKnockRangeAsyncCompleteHandler PortKnockRangeAsyncComplete;
        /// <summary>
        /// Occurs when a list port knock scan has been completed.
        /// </summary>
        public event PortKnockListCompleteHandler PortKnockListComplete;
        /// <summary>
        /// Occurs when an asynchronous list port knock scan has been completed.
        /// </summary>
        public event PortKnockListAsyncCompleteHandler PortKnockListAsyncComplete;
        /// <summary>
        /// Occurs when progress has been updated in a scan.
        /// </summary>
        public event ScanProgressChangedHandler ScanProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in an asynchronous scan.
        /// </summary>
        public event ScanAsyncProgressChangedHandler ScanAsyncProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in a range scan.
        /// </summary>
        public event ScanRangeProgressChangedHandler ScanRangeProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in an asynchronous range scan.
        /// </summary>
        public event ScanRangeAsyncProgressChangedHandler ScanRangeAsyncProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in a list scan.
        /// </summary>
        public event ScanListProgressChangedHandler ScanListProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in an asynchronous list scan.
        /// </summary>
        public event ScanListAsyncProgressChangedHandler ScanListAsyncProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in a port knock scan.
        /// </summary>
        public event PortKnockProgressChangedHandler PortKnockProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in an asynchronous port knock scan.
        /// </summary>
        public event PortKnockAsyncProgressChangedHandler PortKnockAsyncProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in a range port knock scan.
        /// </summary>
        public event PortKnockRangeProgressChangedHandler PortKnockRangeProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in an asynchronous range port knock scan.
        /// </summary>
        public event PortKnockRangeAsyncProgressChangedHandler PortKnockRangeAsyncProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in a list port knock scan.
        /// </summary>
        public event PortKnockListProgressChangedHandler PortKnockListProgressChanged;
        /// <summary>
        /// Occurs when progress has been updated in an asynchronous list port knock scan.
        /// </summary>
        public event PortKnockListAsyncProgressChangedHandler PortKnockListAsyncProgressChanged;

        private void UpdateScanResults(IPScanObject result)
        {
            if (ScanComplete == null) return;
            ScanCompleteEventArgs args = new ScanCompleteEventArgs(result);
            ScanComplete(this, args);
        }
        private void UpdateScanAsyncResults(IPScanObject result)
        {
            if (ScanAsyncComplete == null) return;
            ScanAsyncCompleteEventArgs args = new ScanAsyncCompleteEventArgs(result);
            ScanAsyncComplete(this, args);
        }
        
        private void UpdateScanRangeResults(List<IPScanObject> results)
        {
            if (ScanRangeComplete == null) return;
            ScanRangeCompleteEventArgs args = new ScanRangeCompleteEventArgs(results);
            ScanRangeComplete(this, args);
        }
        private void UpdateScanRangeAsyncResults(List<IPScanObject> results)
        {
            if (ScanRangeAsyncComplete == null) return;
            ScanRangeAsyncCompleteEventArgs args = new ScanRangeAsyncCompleteEventArgs(results);
            ScanRangeAsyncComplete(this, args);
        }
        private void UpdateScanListResults(List<IPScanObject> results)
        {
            if (ScanListComplete == null) return;
            ScanListCompleteEventArgs args = new ScanListCompleteEventArgs(results);
            ScanListComplete(this, args);
        }
        private void UpdateScanListAsyncResults(List<IPScanObject> results)
        {
            if (ScanListAsyncComplete == null) return;
            ScanListAsyncCompleteEventArgs args = new ScanListAsyncCompleteEventArgs(results);
            ScanListAsyncComplete(this, args);
        }
        private void UpdatePortKnockResults(PKScanObject result)
        {
            if (PortKnockComplete == null) return;
            PortKnockCompleteEventArgs args = new PortKnockCompleteEventArgs(result);
            PortKnockComplete(this, args);
        }
        private void UpdatePortKnockAsyncResults(PKScanObject result)
        {
            if (PortKnockAsyncComplete == null) return;
            PortKnockAsyncCompleteEventArgs args = new PortKnockAsyncCompleteEventArgs(result);
            PortKnockAsyncComplete(this, args);
        }
        private void UpdatePortKnockRangeResults(List<PKScanObject> results)
        {
            if (PortKnockRangeComplete == null) return;
            PortKnockRangeCompleteEventArgs args = new PortKnockRangeCompleteEventArgs(results);
            PortKnockRangeComplete(this, args);
        }
        private void UpdatePortKnockRangeAsyncResults(List<PKScanObject> results)
        {
            if (PortKnockRangeAsyncComplete == null) return;
            PortKnockRangeAsyncCompleteEventArgs args = new PortKnockRangeAsyncCompleteEventArgs(results);
            PortKnockRangeAsyncComplete(this, args);
        }
        private void UpdatePortKnockListResults(List<PKScanObject> results)
        {
            if (PortKnockListComplete == null) return;
            PortKnockListCompleteEventArgs args = new PortKnockListCompleteEventArgs(results);
            PortKnockListComplete(this, args);
        }
        private void UpdatePortKnockListAsyncResults(List<PKScanObject> results)
        {
            if (PortKnockListAsyncComplete == null) return;
            PortKnockListAsyncCompleteEventArgs args = new PortKnockListAsyncCompleteEventArgs(results);
            PortKnockListAsyncComplete(this, args);
        }
        private void UpdateScanProgress(int progress)
        {
            if (ScanProgressChanged == null) return;
            ScanProgressChangedEventArgs args = new ScanProgressChangedEventArgs(progress);
            ScanProgressChanged(this, args);
        }
        private void UpdateScanAsyncProgress(int progress)
        {
            if (ScanAsyncProgressChanged == null) return;
            ScanAsyncProgressChangedEventArgs args = new ScanAsyncProgressChangedEventArgs(progress);
            ScanAsyncProgressChanged(this, args);
        }
        private void UpdateScanRangeProgress(int progress, string current = "Unknown")
        {
            if (ScanRangeProgressChanged == null) return;
            ScanRangeProgressChangedEventArgs args = new ScanRangeProgressChangedEventArgs(progress, current);
            ScanRangeProgressChanged(this, args);
        }
        private void UpdateScanRangeAsyncProgress(int progress, string current = "Unknown")
        {
            if (ScanRangeAsyncProgressChanged == null) return;
            ScanRangeAsyncProgressChangedEventArgs args = new ScanRangeAsyncProgressChangedEventArgs(progress, current);
            ScanRangeAsyncProgressChanged(this, args);
        }
        private void UpdateScanListProgress(int progress, string current = "Unknown")
        {
            if (ScanListProgressChanged == null) return;
            ScanListProgressChangedEventArgs args = new ScanListProgressChangedEventArgs(progress, current);
            ScanListProgressChanged(this, args);
        }
        private void UpdateScanListAsyncProgress(int progress, string current = "Unknown")
        {
            if (ScanListAsyncProgressChanged == null) return;
            ScanListAsyncProgressChangedEventArgs args = new ScanListAsyncProgressChangedEventArgs(progress, current);
            ScanListAsyncProgressChanged(this, args);
        }
        private void UpdatePortKnockProgress(int progress)
        {
            if (PortKnockProgressChanged == null) return;
            PortKnockProgressChangedEventArgs args = new PortKnockProgressChangedEventArgs(progress);
            PortKnockProgressChanged(this, args);
        }
        private void UpdatePortKnockAsyncProgress(int progress)
        {
            if (PortKnockAsyncProgressChanged == null) return;
            PortKnockAsyncProgressChangedEventArgs args = new PortKnockAsyncProgressChangedEventArgs(progress);
            PortKnockAsyncProgressChanged(this, args);
        }
        private void UpdatePortKnockRangeProgress(int progress, string current = "Unknown")
        {
            if (PortKnockRangeProgressChanged == null) return;
            PortKnockRangeProgressChangedEventArgs args = new PortKnockRangeProgressChangedEventArgs(progress, current);
            PortKnockRangeProgressChanged(this, args);
        }
        private void UpdatePortKnockRangeAsyncProgress(int progress, string current = "Unknown")
        {
            if (PortKnockRangeAsyncProgressChanged == null) return;
            PortKnockRangeAsyncProgressChangedEventArgs args = new PortKnockRangeAsyncProgressChangedEventArgs(progress, current);
            PortKnockRangeAsyncProgressChanged(this, args);
        }
        private void UpdatePortKnockListProgress(int progress, string current = "Unknown")
        {
            if (PortKnockListProgressChanged == null) return;
            PortKnockListProgressChangedEventArgs args = new PortKnockListProgressChangedEventArgs(progress, current);
            PortKnockListProgressChanged(this, args);
        }
        private void UpdatePortKnockListAsyncProgress(int progress, string current = "Unknown")
        {
            if (PortKnockListAsyncProgressChanged == null) return;
            PortKnockListAsyncProgressChangedEventArgs args = new PortKnockListAsyncProgressChangedEventArgs(progress, current);
            PortKnockListAsyncProgressChanged(this, args);
        }

        #endregion
        #region Initialization

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Scanner() { Fetchers = new ScanFetchers(); }
        /// <summary>
        /// Allows the retrieval of information types through a <see cref="ScanFetchers"/> object.
        /// </summary>
        /// <param name="fetchers"></param>
        public Scanner(ScanFetchers fetchers) { Fetchers = fetchers; }

        #endregion
        #region Methods

        #region Scanning

        /// <summary>
        /// Synchronously scan a single local IP address in order to obtain more information about it.
        /// </summary>
        /// <param name="ip">The endpoint to enumerate information from.</param>
        /// <returns></returns>
        public IPScanObject Scan(string ip)
        {
            // Create a stopwatch to track elapsed time.
            Stopwatch time = Stopwatch.StartNew();

            // Create a blank scan object for our return.
            IPScanObject result = new IPScanObject(ip, 0, "n/a", "n/a", null, false);

            try
            {
                // Create object shells and try to fill them with data.
                long ping = 0;
                string hostname = "n/a";
                string mac = "n/a";
                PKScanObject ports = new PKScanObject();
                bool online = false;

                //Ping our IP.
                if (Fetchers.Ping) { ping = GetAveragePingResponse(ip, Probes, Timeout); }
                UpdateScanProgress(10);
                UpdateScanAsyncProgress(10);

                // Get our hostname.
                if (Fetchers.Hostname)
                {
                    IPHostEntry entry = Dns.GetHostEntry(ip);
                    if (entry != null) { hostname = entry.HostName; }
                }
                UpdateScanProgress(35);
                UpdateScanAsyncProgress(35);

                // Get our MAC address.
                if (Fetchers.MAC)
                {
                    ArpRequestResult request = ArpRequest.Send(IPAddress.Parse(ip));
                    if (request.Exception != null) { mac = "n/a"; }
                    else
                    {
                        // Format our address before passing it.
                        int start = 0;
                        string output = null;
                        while (start < request.Address.ToString().Length)
                        {
                            output += request.Address.ToString().Substring(start, Math.Min(2, request.Address.ToString().Length - start)) + ":";
                            start += 2;
                        }
                        mac = output.Remove(output.Length - 1, 1);
                    }
                }
                UpdateScanProgress(58);
                UpdateScanAsyncProgress(58);

                // Port knock all ports.
                // Let the ports be null for now since it's CPU intensive.
                UpdateScanProgress(75);
                UpdateScanAsyncProgress(75);

                // Set our online flag.
                if (Fetchers.Online) { if (IsHostAlive(ip, Probes, Timeout)) { online = true; } }
                UpdateScanProgress(100);
                UpdateScanAsyncProgress(100);

                // Create a new scan object with our results.
                time.Stop();
                result = new IPScanObject(ip, ping, hostname, mac, ports, online);
            }
            catch (Exception ex) { result.Errors = ex; } // Let it return a blank object containing errors.

            // Return our scanned object even if it's an error shell.
            result.Elapsed = time.Elapsed.TotalSeconds;
            UpdateScanResults(result);
            UpdateScanAsyncResults(result);
            return result;
        }

        /// <summary>
        /// Asynchronously scan a single local IP address in order to obtain more information about it.
        /// </summary>
        /// <param name="ip">The endpoint to enumerate information from.</param>
        /// <returns></returns>
        public async Task<IPScanObject> ScanAsync(string ip)
        {
            TaskFactory task = new TaskFactory();
            return await task.StartNew(() => Scan(ip));
        }

        /// <summary>
        /// Synchronously scan a subsequent range of IP addresses for information.
        /// </summary>
        /// <param name="ip1">The beginning endpoint that intializes the scan.</param>
        /// <param name="ip2">The ending endpoint that the scan will stop after.</param>
        public List<IPScanObject> ScanRange(string ip1, string ip2)
        {
            // Keep track of our scan progress.
            int progress = 0;

            // Create our list of scan objects to return.
            List<IPScanObject> results = new List<IPScanObject>();

            try
            {
                // Parse our IPs into an IPAddressRange object.
                IPAddress address1 = IPAddress.Parse(ip1);
                IPAddress address2 = IPAddress.Parse(ip2);
                IPAddressRange range = new IPAddressRange(address1, address2);

                // Loop through our range and begin scanning each IP.
                int total = 0; foreach (IPAddress x in range) { total++; } // Grab number of addresses before proceeding.
                int count = 0; // Amount of addresses scanned.
                foreach (IPAddress address in range)
                {
                    // Iterate up so we can report progress to the user.
                    count++;

                    // Let the user know which address we are currently scanning.
                    UpdateScanRangeProgress(progress, address.ToString());
                    UpdateScanRangeAsyncProgress(progress, address.ToString());

                    // Scan our current address.
                    IPScanObject result = Scan(address.ToString());
                    results.Add(result);

                    // Calculate our progress percentage.
                    int x = (int)(((double)count / (double)total) * 100);
                    if (x > progress)
                    {
                        progress = x;
                        UpdateScanRangeProgress(progress, address.ToString());
                        UpdateScanRangeAsyncProgress(progress, address.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Create a new scan object containing our errors instead of returning null.
                IPScanObject result = new IPScanObject();
                result.Errors = ex;
                results.Add(result);
                return results;
            }

            // Update our completed event.
            UpdateScanRangeResults(results);
            UpdateScanRangeAsyncResults(results);

            // Return our list of scanned IP addresses.
            return results;
        }

        /// <summary>
        /// Asynchronously scan a subsequent range of IP addresses for information.
        /// </summary>
        /// <param name="ip1">The starting endpoint that initializes the scan.</param>
        /// <param name="ip2">The ending endpoint that the scan will stop after.</param>
        /// <returns></returns>
        public async Task<List<IPScanObject>> ScanRangeAsync(string ip1, string ip2)
        {
            TaskFactory task = new TaskFactory();
            return await task.StartNew((() => ScanRange(ip1, ip2)));
        }

        /// <summary>
        /// Synchronously scan a pre-formatted list of IP addresses for information.
        /// </summary>
        /// <param name="list">A list of IPs formatted as such: "address1, address2, address3, ..."</param>
        public List<IPScanObject> ScanList(string list)
        {
            // Track our progress.
            int progress = 0;

            // Create a list of scan objects to return.
            List<IPScanObject> results = new List<IPScanObject>();

            try
            {
                // Trim all whitespace from our list of addresses.
                string trimmed = Regex.Replace(list, @"\s+", "");

                // Split our list into an array so we can access each address individually.
                string[] addresses = trimmed.Split(',');

                // Loop through our addresses and begin scanning each one.
                int count = 0;
                foreach (string address in addresses)
                {
                    // Iterate up so we can report progress to the user.
                    count++;

                    // Let the user know which address we are currently scanning.
                    UpdateScanListProgress(progress, address);
                    UpdateScanListAsyncProgress(progress, address);

                    // Scan our current address.
                    IPScanObject result = Scan(address);
                    results.Add(result);

                    // Update our progress percentage.
                    int x = (int)(((double)count / (double)addresses.Length) * 100);
                    if (x > progress)
                    {
                        progress = x;
                        UpdateScanListProgress(progress, address);
                        UpdateScanListAsyncProgress(progress, address);
                    }
                }
            }
            catch (Exception ex) // Catch if we are unable to parse the list.
            {
                // Create a new scan object containing our errors instead of returning null.
                IPScanObject result = new IPScanObject();
                result.Errors = ex;
                results.Add(result);
                return results;
            }

            // Update our event with the results.
            UpdateScanListResults(results);
            UpdateScanListAsyncResults(results);

            // Return our list of scanned IP addresses.
            return results;
        }

        /// <summary>
        /// Asynchronously scan a pre-formatted list of IP addresses for information.
        /// </summary>
        /// <param name="list">A list of IPs formatted as such: "address1, address2, address3, ..."</param>
        /// <returns></returns>
        public async Task<List<IPScanObject>> ScanListAsync(string list)
        {
            TaskFactory task = new TaskFactory();
            return await task.StartNew(() => ScanList(list));
        }

        /// <summary>
        /// Synchronoulsy scan an endpoint for open ports and their services.
        /// </summary>
        /// <param name="ip">The endpoint that should be scanned.</param>
        public PKScanObject PortKnock(string ip)
        {
            // Track our progress.
            int progress = 0;

            // Create a new scan object to return.
            PKScanObject result = new PKScanObject();

            try
            {
                // Parse our string IP into an IPAddress object.
                IPAddress address = IPAddress.Parse(ip);

                // Check all ports by using both TCP and UDP.
                for (int i = 1; i < 65536; i++)
                {
                    bool status = false;
                    PKServiceObject port = new PKServiceObject();

                    // Check port using TCP.
                    status = IsPortOpen(ip, i, new TimeSpan(250), false);
                    if (status) { port = new PKServiceObject(ip, i, PortType.TCP, status); }

                    // Check port using UDP. <<------ Produces a result of all 65535 ports being open/alive.
                    //status = IsPortOpen(ip, i, new TimeSpan(5000), true);
                    //if (status) { port = new PKServiceObject(ip, i, PortType.UDP, "", status); }

                    // Add any open ports to our scan object.
                    result.Services.Add(port);

                    // Update our port knock progress events.
                    int x = (int)(((double)i / (double)65536) * 100);
                    if (x > progress)
                    {
                        progress = x;
                        UpdatePortKnockProgress(progress);
                        UpdatePortKnockAsyncProgress(progress);
                    }
                }
            }
            catch (Exception ex) { result.Errors = ex; }

            // Update our port knock complete events.
            UpdatePortKnockResults(result);
            UpdatePortKnockAsyncResults(result);
            return result;
        }

        /// <summary>
        /// Asynchronously scan an endpoint for open ports and their services.
        /// </summary>
        /// <param name="ip">The endpoint that should be scanned.</param>
        /// <returns></returns>
        public async Task<PKScanObject> PortKnockAsync(string ip)
        {
            TaskFactory task = new TaskFactory();
            return await task.StartNew(() => PortKnock(ip));
        }

        /// <summary>
        /// Synchronously scan a subsequent range of endpoints for open ports and their services.
        /// </summary>
        /// <param name="ip1">The starting endpoint which initializaes the scan.</param>
        /// <param name="ip2">The ending endpoint which the scan will stop after.</param>
        public List<PKScanObject> PortKnockRange(string ip1, string ip2)
        {
            // Track our progress.
            int progress = 0;

            // Create a list of scan objects to return.
            List<PKScanObject> results = new List<PKScanObject>();

            try
            {
                // Parse our IPs into an IPAddressRange object.
                IPAddress address1 = IPAddress.Parse(ip1);
                IPAddress address2 = IPAddress.Parse(ip2);
                IPAddressRange range = new IPAddressRange(address1, address2);

                // Loop through our range of IPs and start port knocking.
                int total = 0; foreach (IPAddress x in range) { total++; } // Grab number of addresses before proceeding.
                int count = 0; // Amount of addresses scanned.
                foreach (IPAddress address in range)
                {
                    // Iterate up so we can report progress to the user.
                    count++;

                    // Let the user know which address we are currently scanning.
                    UpdatePortKnockRangeProgress(progress, address.ToString());
                    UpdatePortKnockRangeAsyncProgress(progress, address.ToString());

                    // Scan the current address.
                    PKScanObject result = PortKnock(address.ToString());
                    results.Add(result);

                    // Calculate our progress percentage.
                    int x = (int)(((double)count / (double)total) * 100);
                    if (x > progress)
                    {
                        progress = x;
                        UpdatePortKnockRangeProgress(progress, address.ToString());
                        UpdatePortKnockRangeAsyncProgress(progress, address.ToString());
                    }
                }
            }
            catch (Exception ex) // Catch if we are unable to parse the list.
            {
                // Create a new scan object containing our errors instead of returning null.
                PKScanObject result = new PKScanObject();
                result.Errors = ex;
                results.Add(result);
                return results;
            }

            // Update our event with our results.
            UpdatePortKnockRangeResults(results);
            UpdatePortKnockRangeAsyncResults(results);

            // Return our scanned ip addresses.
            return results;
        }

        /// <summary>
        /// Asynchronously scan a subsequent range of a endpoints for open ports and their services.
        /// </summary>
        /// <param name="ip1">The starting endpoint which initializes the scan.</param>
        /// <param name="ip2">The ending endpoint which the scan will stop after.</param>
        /// <returns></returns>
        public async Task<List<PKScanObject>> PortKnockRangeAsync(string ip1, string ip2)
        {
            TaskFactory task = new TaskFactory();
            return await task.StartNew(() => PortKnockRange(ip1, ip2));
        }

        /// <summary>
        /// Synchronously scan a pre-formatted list of IP addresses for open ports and their services.
        /// </summary>
        /// <param name="list">A list of IPs formatted as such: "address1, address2, address3, ..."</param>
        public List<PKScanObject> PortKnockList(string list)
        {
            // Keep track of our progress.
            int progress = 0;

            // Create a list of scan objects to return.
            List<PKScanObject> results = new List<PKScanObject>();

            try
            {
                // Trim all whitespace from our list of addresses.
                string trimmed = Regex.Replace(list, @"\s+", "");

                // Split our list into an array so we can access each address individually.
                string[] addresses = trimmed.Split(',');

                // Loop through our addresses and begin scanning each one.
                int count = 0; // Amount of addresses scanned.
                foreach (string address in addresses)
                {
                    // Iterate up to display our progress.
                    count++;

                    // Tell the user what address we're currently scanning.
                    UpdateScanListProgress(progress, address.ToString());
                    UpdateScanListAsyncProgress(progress, address.ToString());

                    // Scan our current address.
                    PKScanObject result = PortKnock(address);
                    results.Add(result);

                    // Calculate our progress percentage.
                    int x = (int)(((double)count / (double)addresses.Length) * 100);
                    if (x > progress)
                    {
                        progress = x;
                        UpdateScanListProgress(progress, address.ToString());
                        UpdateScanListAsyncProgress(progress, address.ToString());
                    }
                }
            }
            catch (Exception ex) // Catch if we are unable to parse the list.
            {
                // Create a new scan object containing our errors instead of returning null.
                PKScanObject result = new PKScanObject();
                result.Errors = ex;
                results.Add(result);
                return results;
            }

            // Update our event with the results.
            UpdatePortKnockListResults(results);
            UpdatePortKnockListAsyncResults(results);

            // Return our list of scanned IP addresses.
            return results;
        }

        /// <summary>
        /// Asynchronously scan a pre-formatted list of IP addresses for open ports and their services.
        /// </summary>
        /// <param name="list">A list of IPs formatted as such: "address1, address2, address3, ...</param>
        /// <returns></returns>
        public async Task<List<PKScanObject>> PortKnockListAsync(string list)
        {
            TaskFactory task = new TaskFactory();
            return await task.StartNew(() => PortKnockList(list));
        }

        #endregion
        #region Other

        /// <summary>
        /// Obtain the average time it takes to ping a host with N attempts.
        /// </summary>
        /// <param name="ip">The host that will be pinged.</param>
        /// <param name="attempts">The number of times the host should be pinged.</param>
        /// <param name="timeout">The amount of time to wait for a response before moving on.</param>
        /// <returns></returns>
        private long GetAveragePingResponse(string ip, int attempts, int timeout)
        {
            long average = 0;
            List<long> responses = new List<long>();
            Ping ping = new Ping();
            PingReply reply;

            for (int i = 0; i < attempts; i++)
            {
                try
                {
                    // Send our ping to our ip.
                    reply = ping.Send(ip, timeout);

                    // Add our reply time if we have successfully gotten a reply.
                    if (reply != null && reply.Status == IPStatus.Success)
                        responses.Add(reply.RoundtripTime);
                }
                catch { } // Suppress errors in case of dropped packets.
            }

            // Calculate our average response time and return it.
            foreach (long response in responses) { average += response; }
            average = average / responses.Count;
            return average;
        }

        /// <summary>
        /// Check if a host is up and reachable through the network.
        /// </summary>
        /// <param name="ip">The host that will be pinged.</param>
        /// <param name="attempts">The amount of times that the host should be pinged.</param>
        /// <param name="timeout">The amount of time to wait for a response before moving on.</param>
        /// <returns></returns>
        private bool IsHostAlive(string ip, int attempts, int timeout)
        {
            Ping ping = new Ping();
            PingReply reply;

            for (int i = 0; i < attempts; i++)
            {
                try
                {
                    // Send our ping to our ip.
                    reply = ping.Send(ip, timeout);

                    // Return true if we have successfully gotten a reply.
                    if (reply != null && reply.Status == IPStatus.Success)
                        return true;
                }
                catch { } // Suppress errors in case of dropped packets.
            }
            
            // Return false if we're unable to reach the host.
            return false;
        }

        /// <summary>
        /// Check if a port is open on a host using TCP or UDP.
        /// </summary>
        /// <param name="ip">The host that will be scanned.</param>
        /// <param name="port">The port number that will be checked.</param>
        /// <param name="timeout">The amount of time that should be expeneded before moving on.</param>
        /// <param name="useUDP">Use UDP packets instead of TCP to ping the host.</param>
        /// <returns></returns>
        private bool IsPortOpen(string ip, int port, TimeSpan timeout, bool useUDP = false)
        {
            if (!useUDP)
            {
                try
                {
                    using (var client = new TcpClient())
                    {
                        var result = client.BeginConnect(ip, port, null, null);
                        var success = result.AsyncWaitHandle.WaitOne(timeout);
                        if (!success) { return false; }
                        client.EndConnect(result);
                    }
                }
                catch { return false; }
            }
            else
            {
                try
                {
                    using (var client = new UdpClient())
                    {
                        client.Connect(ip, port);
                        client.Close();
                    }
                }
                catch { return false; }
            }

            return true;
        }

        #endregion

        #endregion
    }
}
