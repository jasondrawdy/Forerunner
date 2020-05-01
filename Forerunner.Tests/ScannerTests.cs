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

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion
namespace Forerunner.Tests
{
    [TestClass]
    public class ScannerTests
    {
        // Synchronous methods that should return null or a default shell.
        [TestMethod]
        public void Scan_With_Invalid_IP()
        {
            // Setup our scanner.
            Scanner s = new Scanner();
            s.ScanProgressChanged += (Scan_ProgressChanged_With_Invalid_IP);
            s.ScanComplete += new ScanCompleteHandler(Scan_Complete_With_Invalid_IP);
            
            // Run our scan.
            IPScanObject expected = new IPScanObject("N/A", 0, "N/A", "N/A", null, false);
            IPScanObject actual = s.Scan("x.x.x.x");

            // Make sure we get what we want.
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void ScanRange_With_Invalid_IPAddressRange()
        {
            // Setup our scanner.
            Scanner s = new Scanner();
            s.ScanRangeProgressChanged += new ScanRangeProgressChangedHandler(ScanRange_ProgressChanged_With_Invalid_IPAddressRange);
            s.ScanRangeComplete += new ScanRangeCompleteHandler(ScanRange_Complete_With_Invalid_IPAddressRange);

            // Run our scan.
            List<IPScanObject> expected = new List<IPScanObject>();
            List<IPScanObject> actual = s.ScanRange("x.x.x.x", "y.y.y.y");

            // Make sure we get what we want.
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void ScanList_With_Invalid_List()
        {
            // Setup our scanner.
            Scanner s = new Scanner();
            s.ScanListProgressChanged += new ScanListProgressChangedHandler(ScanList_ProgressChanged_With_Invalid_List);
            s.ScanListComplete += new ScanListCompleteHandler(ScanList_Complete_With_Invalid_List);

            // Run our scan.
            List<IPScanObject> expected = new List<IPScanObject>();
            List<IPScanObject> actual = s.ScanList("x.x.x.x, y.y.y.y, z.z.z.z");

            // Make sure we get what we want.
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void PortKnock_With_Invalid_IP()
        {
            // Setup our scanner.
            Scanner s = new Scanner();
            s.PortKnockProgressChanged += new PortKnockProgressChangedHandler(PortKnock_ProgressChanged_With_Invalid_IP);
            s.PortKnockComplete += new PortKnockCompleteHandler(PortKnock_Complete_With_Invalid_IP);

            // Run our scan.
            PKScanObject expected = new PKScanObject();
            PKScanObject actual = s.PortKnock("x.x.x.x");

            // Make sure that we get what we want.
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void PortKnockRange_With_Invalid_IPAddressRange()
        {
            // Setup our scanner.
            Scanner s = new Scanner();
            s.PortKnockRangeProgressChanged += new PortKnockRangeProgressChangedHandler(PortKnockRange_ProgressChanged_With_Invalid_IPAddressRange);
            s.PortKnockRangeComplete += new PortKnockRangeCompleteHandler(PortKnockRange_Complete_With_Invalid_IPAddressRange);

            // Run our scan.
            List<PKScanObject> expected = new List<PKScanObject>();
            List<PKScanObject> actual = s.PortKnockRange("x.x.x.x", "y.y.y.y");

            // Make sure that we get what we want.
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void PortKnockList_With_Invalid_IPAddressRange()
        {
            // Setup our scanner.
            Scanner s = new Scanner();
            s.PortKnockListProgressChanged += new PortKnockListProgressChangedHandler(PortKnockList_ProgressChanged_With_Invalid_List);
            s.PortKnockListComplete += new PortKnockListCompleteHandler(PortKnockList_Complete_With_Invalid_List);

            /// Run our scan.
            List<PKScanObject> expected = new List<PKScanObject>();
            List<PKScanObject> actual = s.PortKnockList("x.x.x.x, y.y.y.y, z.z.z.z");

            // Make sure that we get what we want.
            Assert.AreNotEqual(expected, actual);
        }
        
        // Completed method event handlers that should return null an empty shell.
        [TestMethod]
        public void Scan_Complete_With_Invalid_IP(object sender, ScanCompleteEventArgs e)
        {
            // Actual should be a default shell.
            IPScanObject expected = new IPScanObject("N/A", 0, "N/A", "N/A", null, false);
            IPScanObject actual = e.Result;

            // Make sure we get what we want.
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ScanRange_Complete_With_Invalid_IPAddressRange(object sender, ScanRangeCompleteEventArgs e)
        {
            // Actual should be a blank list.
            List<IPScanObject> expected = new List<IPScanObject>();
            List<IPScanObject> actual = e.Results;

            // Make sure we get what we want.
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ScanList_Complete_With_Invalid_List(object sender, ScanListCompleteEventArgs e)
        {
            // Actual should be a blank list.
            List<IPScanObject> expected = new List<IPScanObject>();
            List<IPScanObject> actual = e.Results;

            // Make sure we get what we want.
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PortKnock_Complete_With_Invalid_IP(object sender, PortKnockCompleteEventArgs e)
        {
            // Actual should be a blank object.
            PKScanObject expected = new PKScanObject();
            PKScanObject actual = e.Result;

            // Make sure we get what we want.
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PortKnockRange_Complete_With_Invalid_IPAddressRange(object sender, PortKnockRangeCompleteEventArgs e)
        {
            // Actual should be a blank list.
            List<PKScanObject> expected = new List<PKScanObject>();
            List<PKScanObject> actual = e.Results;

            // Make sure we get what we want.
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PortKnockList_Complete_With_Invalid_List(object sender, PortKnockListCompleteEventArgs e)
        {
            // Actual should be a blank list.
            List<PKScanObject> expected = new List<PKScanObject>();
            List<PKScanObject> actual = e.Results;

            // Make sure we get what we want.
            Assert.AreEqual(expected, actual);
        }

        // Progress changed event handlers.
        [TestMethod]
        public void Scan_ProgressChanged_With_Invalid_IP(object sender, ScanProgressChangedEventArgs e) { }
        [TestMethod]
        public void ScanRange_ProgressChanged_With_Invalid_IPAddressRange(object sender, ScanRangeProgressChangedEventArgs e) { }
        [TestMethod]
        public void ScanList_ProgressChanged_With_Invalid_List(object sender, ScanListProgressChangedEventArgs e) { }
        [TestMethod]
        public void PortKnock_ProgressChanged_With_Invalid_IP(object sender, PortKnockProgressChangedEventArgs e) { }
        [TestMethod]
        public void PortKnockRange_ProgressChanged_With_Invalid_IPAddressRange(object sender, PortKnockRangeProgressChangedEventArgs e) { }
        [TestMethod]
        public void PortKnockList_ProgressChanged_With_Invalid_List(object sender, PortKnockListProgressChangedEventArgs e) { }
    }
}
