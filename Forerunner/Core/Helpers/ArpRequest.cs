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
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

#endregion
namespace Forerunner
{
    /// <summary>Provides methods for sending requests through the ARP protocol.</summary>
    public static class ArpRequest
    {
        /// <summary>
        /// Sends a request via the ARP protocol to resolve an IP address into the physical address. If the physical address is already in the cache of the host, this is returned.
        /// </summary>
        /// <param name="destination">Destination <see cref="IPAddress"/>.</param>
        /// <returns>A <see cref="T:System.Net.ArpRequestResult">ArpRequestResult</see>-Instance containing the results of the request.</returns>
        public static ArpRequestResult Send(IPAddress destination)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            int destIp = BitConverter.ToInt32(destination.GetAddressBytes(), 0);

            var addr = new byte[6];
            var len = addr.Length;

            var res = NativeMethods.SendARP(destIp, 0, addr, ref len);

            if (res == 0)
                return new ArpRequestResult(new PhysicalAddress(addr));
            return new ArpRequestResult(new Win32Exception(res));
        }
    }
}
