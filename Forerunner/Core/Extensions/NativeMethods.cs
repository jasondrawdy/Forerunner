/*
==============================================================================
Copyright © Jason Drawdy (CloneMerge)

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

using System.Security;
using System.Runtime.InteropServices;

#endregion
namespace Forerunner
{
    /// <summary>
    /// Provides access to native Windows methods through a managed interface.
    /// </summary>
    internal static class NativeMethods
    {
        private const string IphlpApi = "iphlpapi.dll";
        /// <summary>
        /// Sends an ARP request to a specific IP address in order to resolve to its corresponding physical address.
        /// </summary>
        /// <param name="destinationIp">The IP address to send the request to.</param>
        /// <param name="sourceIp">The IP address that is sending the request.</param>
        /// <param name="macAddress">An array the physical address to be resolved to.</param>
        /// <param name="physicalAddrLength"></param>
        /// <returns></returns>
        [DllImport(IphlpApi, ExactSpelling = true)]
        [SecurityCritical]
        internal static extern int SendARP(int destinationIp, int sourceIp, byte[] macAddress, ref int physicalAddrLength);
    }
}
