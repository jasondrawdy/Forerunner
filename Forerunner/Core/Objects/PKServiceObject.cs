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

namespace Forerunner
{
    /// <summary>
    /// An enumeration of common data transfer protocols.
    /// </summary>
    public enum PortType { TCP, UDP }

    public class PKServiceObject
    {
        #region Variables

        /// <summary>
        /// Initial IP address that was scanned.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Initial port that was scanned.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The transfer protocol that the port is running on.
        /// </summary>
        public PortType Protocol { get; set; }

        /// <summary>
        /// Status of the port itself; open or closed.
        /// </summary>
        public bool Status { get; set; }

        #endregion
        #region Initialization

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PKServiceObject() { }

        /// <summary>
        /// Main Constructor.
        /// </summary>
        /// <param name="ip">Initial ip that was scanned.</param>
        /// <param name="port">Initial port that was scanned.</param>
        /// <param name="protocol">The transfer protocol that the port is running on.</param>
        /// <param name="status">Status of the port itself; open or closed.</param>
        public PKServiceObject(string ip, int port, PortType protocol, bool status)
        {
            IP = ip;
            Port = port;
            Protocol = protocol;
            Status = status;
        }

        #endregion
    }
}
