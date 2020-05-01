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
using System.Collections.Generic;

#endregion
namespace Forerunner
{
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanCompleteHandler(object sender, ScanCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanAsyncComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanAsyncCompleteHandler(object sender, ScanAsyncCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanRangeComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanRangeCompleteHandler(object sender, ScanRangeCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanRangeAsyncComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanRangeAsyncCompleteHandler(object sender, ScanRangeAsyncCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanListComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanListCompleteHandler(object sender, ScanListCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanListAsyncComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanListAsyncCompleteHandler(object sender, ScanListAsyncCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockCompleteHandler(object sender, PortKnockCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockAsyncComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockAsyncCompeleteHandler(object sender, PortKnockAsyncCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockRangeComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockRangeCompleteHandler(object sender, PortKnockRangeCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockRangeAsyncComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockRangeAsyncCompleteHandler(object sender, PortKnockRangeAsyncCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockListComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockListCompleteHandler(object sender, PortKnockListCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockListAsyncComplete"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockListAsyncCompleteHandler(object sender, PortKnockListAsyncCompleteEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanProgressChangedHandler(object sender, ScanProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanAsyncProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanAsyncProgressChangedHandler(object sender, ScanAsyncProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanRangeProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanRangeProgressChangedHandler(object sender, ScanRangeProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanRangeAsyncProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanRangeAsyncProgressChangedHandler(object sender, ScanRangeAsyncProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanListProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanListProgressChangedHandler(object sender, ScanListProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.ScanListAsyncProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void ScanListAsyncProgressChangedHandler(object sender, ScanListAsyncProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockProgressChangedHandler(object sender, PortKnockProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockAsyncProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockAsyncProgressChangedHandler(object sender, PortKnockAsyncProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockRangeProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockRangeProgressChangedHandler(object sender, PortKnockRangeProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockRangeAsyncProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockRangeAsyncProgressChangedHandler(object sender, PortKnockRangeAsyncProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockListProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockListProgressChangedHandler(object sender, PortKnockListProgressChangedEventArgs e);
    /// <summary>
    /// Represents the method that will handle the <see cref="Scanner.PortKnockListAsyncProgressChanged"/> event of a <see cref="Scanner"/> object.
    /// </summary>
    public delegate void PortKnockListAsyncProgressChangedHandler(object sender, PortKnockListAsyncProgressChangedEventArgs e);

    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanComplete"/> event.
    /// </summary>
    public class ScanCompleteEventArgs : EventArgs
    {
        public IPScanObject Result { get; private set; }
        public ScanCompleteEventArgs(IPScanObject result)
        {
            Result = result;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanAsyncComplete"/> event.
    /// </summary>
    public class ScanAsyncCompleteEventArgs : EventArgs
    {
        public IPScanObject Result { get; private set; }
        public ScanAsyncCompleteEventArgs(IPScanObject result)
        {
            Result = result;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanRangeComplete"/> event.
    /// </summary>
    public class ScanRangeCompleteEventArgs : EventArgs
    {
        public List<IPScanObject> Results { get; private set; }
        public ScanRangeCompleteEventArgs(List<IPScanObject> results)
        {
            Results = results;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanRangeAsyncComplete"/> event.
    /// </summary>
    public class ScanRangeAsyncCompleteEventArgs : EventArgs
    {
        public List<IPScanObject> Results { get; private set; }
        public ScanRangeAsyncCompleteEventArgs(List<IPScanObject> results)
        {
            Results = results;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanListComplete"/> event.
    /// </summary>
    public class ScanListCompleteEventArgs : EventArgs
    {
        public List<IPScanObject> Results { get; private set; }
        public ScanListCompleteEventArgs(List<IPScanObject> results)
        {
            Results = results;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanListAsyncComplete"/> event.
    /// </summary>
    public class ScanListAsyncCompleteEventArgs : EventArgs
    {
        public List<IPScanObject> Results { get; private set; }
        public ScanListAsyncCompleteEventArgs(List<IPScanObject> results)
        {
            Results = results;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockComplete"/> event.
    /// </summary>
    public class PortKnockCompleteEventArgs : EventArgs
    {
        public PKScanObject Result { get; private set; }
        public PortKnockCompleteEventArgs(PKScanObject result)
        {
            Result = result;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockAsyncComplete"/> event.
    /// </summary>
    public class PortKnockAsyncCompleteEventArgs : EventArgs
    {
        public PKScanObject Result { get; private set; }
        public PortKnockAsyncCompleteEventArgs(PKScanObject result)
        {
            Result = result;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockRangeComplete"/> event.
    /// </summary>
    public class PortKnockRangeCompleteEventArgs : EventArgs
    {
        public List<PKScanObject> Results { get; private set; }
        public PortKnockRangeCompleteEventArgs(List<PKScanObject> results)
        {
            Results = results;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockRangeAsyncComplete"/> event.
    /// </summary>
    public class PortKnockRangeAsyncCompleteEventArgs : EventArgs
    {
        public List<PKScanObject> Results { get; private set; }
        public PortKnockRangeAsyncCompleteEventArgs(List<PKScanObject> results)
        {
            Results = results;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockListComplete"/> event.
    /// </summary>
    public class PortKnockListCompleteEventArgs : EventArgs
    {
        public List<PKScanObject> Results { get; private set; }
        public PortKnockListCompleteEventArgs(List<PKScanObject> results)
        {
            Results = results;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockListAsyncComplete"/> event.
    /// </summary>
    public class PortKnockListAsyncCompleteEventArgs : EventArgs
    {
        public List<PKScanObject> Results { get; private set; }
        public PortKnockListAsyncCompleteEventArgs(List<PKScanObject> results)
        {
            Results = results;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanProgressChanged"/> event.
    /// </summary>
    public class ScanProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public ScanProgressChangedEventArgs(int progress)
        {
            Progress = progress;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanAsyncProgressChanged"/> event.
    /// </summary>
    public class ScanAsyncProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public ScanAsyncProgressChangedEventArgs(int progress)
        {
            Progress = progress;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanRangeProgressChanged"/> event.
    /// </summary>
    public class ScanRangeProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public string CurrentlyScanning { get; private set; }
        public ScanRangeProgressChangedEventArgs(int progress, string current = "Unknown")
        {
            Progress = progress;
            CurrentlyScanning = current;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanRangeAsyncProgressChanged"/> event.
    /// </summary>
    public class ScanRangeAsyncProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public string CurrentlyScanning { get; private set; }
        public ScanRangeAsyncProgressChangedEventArgs(int progress, string current = "Unknown")
        {
            Progress = progress;
            CurrentlyScanning = current;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanListProgressChanged"/> event.
    /// </summary>
    public class ScanListProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public string CurrentlyScanning { get; private set; }
        public ScanListProgressChangedEventArgs(int progress, string current = "Unknown")
        {
            Progress = progress;
            CurrentlyScanning = current;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.ScanListAsyncProgressChanged"/> event.
    /// </summary>
    public class ScanListAsyncProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public string CurrentlyScanning { get; private set; }
        public ScanListAsyncProgressChangedEventArgs(int progress, string current = "Unknown")
        {
            Progress = progress;
            CurrentlyScanning = current;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockProgressChanged"/> event.
    /// </summary>
    public class PortKnockProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public PortKnockProgressChangedEventArgs(int progress)
        {
            if (progress > Progress)
                Progress = progress;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockAsyncProgressChanged"/> event.
    /// </summary>
    public class PortKnockAsyncProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public PortKnockAsyncProgressChangedEventArgs(int progress)
        {
            if (progress > Progress)
                Progress = progress;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockRangeProgressChanged"/> event.
    /// </summary>
    public class PortKnockRangeProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public string CurrentlyScanning { get; private set; }
        public PortKnockRangeProgressChangedEventArgs(int progress, string current = "Unknown")
        {
            Progress = progress;
            CurrentlyScanning = current;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockRangeAsyncProgressChanged"/> event.
    /// </summary>
    public class PortKnockRangeAsyncProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public string CurrentlyScanning { get; private set; }
        public PortKnockRangeAsyncProgressChangedEventArgs(int progress, string current = "Unknown")
        {
            Progress = progress;
            CurrentlyScanning = current;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockListProgressChanged"/> event.
    /// </summary>
    public class PortKnockListProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public string CurrentlyScanning { get; private set; }
        public PortKnockListProgressChangedEventArgs(int progress, string current = "Unknown")
        {
            Progress = progress;
            CurrentlyScanning = current;
        }
    }
    /// <summary>
    /// Contains data for the <see cref="Scanner.PortKnockListAsyncProgressChanged"/> event.
    /// </summary>
    public class PortKnockListAsyncProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public string CurrentlyScanning { get; private set; }
        public PortKnockListAsyncProgressChangedEventArgs(int progress, string current = "Unknown")
        {
            Progress = progress;
            CurrentlyScanning = current;
        }
    }
}
