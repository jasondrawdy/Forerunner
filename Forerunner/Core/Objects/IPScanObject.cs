/*
==============================================================================
Copyright © Jason Drawdy 

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

#endregion
namespace Forerunner
{
    public class IPScanObject
    {
        #region Variables

        /// <summary>
        /// The original IP address in a string representation that was provided to the IPScanObject.
        /// </summary>
        public string Address { get; private set; } // The originally scanned IP

        /// <summary>
        /// The original IP address provided to the IPScanObject parsed to an IPAddress object.
        /// </summary>
        public IPAddress IP { get; private set; } // The originally scanned IP

        /// <summary>
        /// The average response time in milliseconds for a data packet to be sent and replied to.
        /// </summary>
        public long Ping { get; private set; } // The average response time for a ping request

        /// <summary>
        /// The name of the device sitting at an endpoint.
        /// </summary>
        public string Hostname { get; private set; } // The actual name of the host

        /// <summary>
        /// The physical address of a device at a specific endpoint.
        /// </summary>
        public string MAC { get; private set; } // The physical address of the computer

        /// <summary>
        /// Collection of scanned ports.
        /// </summary>
        public PKScanObject Ports { get; private set; }

        /// <summary>
        /// The online status of a device at a specific endpoint.
        /// </summary>
        public bool isOnline { get; private set; }

        /// <summary>
        /// Total amount of time for current object's scan to complete.
        /// </summary>
        public double Elapsed { get; set; }

        /// <summary>
        /// General exception object which is set upon an error.
        /// </summary>
        public Exception Errors { get; set; }

        #endregion
        #region Initialization

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public IPScanObject() { }

        /// <summary>
        /// Main Constructor.
        /// </summary>
        /// <param name="address">The original IP.</param>
        /// <param name="pingTime">The average response time in milliseconds for a ping request.</param>
        /// <param name="hostname">The actual name of a device at an endpoint.</param>
        /// <param name="mac">The physical address of a device.</param>
        /// <param name="ports">Collection of scanned ports.</param>
        /// <param name="online">The online status of a device.</param>
        /// <param name="elapsed">Total amount of time for current object's scan to compelte.</param>
        public IPScanObject(string address, long pingTime, string hostname, string mac, PKScanObject ports, bool online, double elapsed = 0)
        {
            Address = address;
            try { IP = IPAddress.Parse(address); } catch { } // The provided IP wasn't formatted correctly.
            Ping = pingTime;
            Hostname = hostname;
            MAC = mac;
            Ports = ports;
            isOnline = online;
            Elapsed = elapsed;
        }

        #endregion
    }
}
