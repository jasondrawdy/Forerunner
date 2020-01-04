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

namespace Forerunner
{
    public class ScanFetchers
    {
        #region Variables

        /// <summary>
        /// Obtain the hostname of the IP being scanned.
        /// </summary>
        public bool Hostname { get; private set; } = true;
        /// <summary>
        /// Obtain the physical address of the IP being scanned.
        /// </summary>
        public bool MAC { get; private set; } = true;
        /// <summary>
        /// Obtain the length of time — in milliseconds — it takes to reach the destination address.
        /// </summary>
        public bool Ping { get; private set; } = true;
        /// <summary>
        /// Obtain whether or not the destination address is available or not.
        /// </summary>
        public bool Online { get; private set; } = true;

        #endregion
        #region Initialization

        /// <summary>
        /// A container of parameters for a <see cref="Scanner"/> object which allows the retrieval of set information types.
        /// </summary>
        /// <param name="hostname">Obtain the hostname of the IP being scanned.</param>
        /// <param name="mac">Obtain the physical address of the IP being scanned.</param>
        /// <param name="ping">Obtain the length of time — in milliseconds — it takes to reach the destination address.</param>
        /// <param name="online">Obtain whether or not the destination address is available or not.</param>
        public ScanFetchers(bool hostname = true, bool mac = true, bool ping = true, bool online = true)
        {
            Hostname = hostname;
            MAC = mac;
            Ping = ping;
            Online = online;
        }

        #endregion
    }
}
