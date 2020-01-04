<p align="center">
    <img width="256" height="256" src="https://image.flaticon.com/icons/svg/776/776246.svg">
</p>

# Forerunner
<p align="left">
    <!-- Version -->
    <img src="https://img.shields.io/badge/version-1.0.0-brightgreen.svg">
    <!-- <img src="https://img.shields.io/appveyor/ci/gruntjs/grunt.svg"> -->
    <!-- Docs -->
    <img src="https://img.shields.io/badge/docs-not%20found-lightgrey.svg">
    <!-- License -->
    <img src="https://img.shields.io/badge/license-MIT-blue.svg">
</p>

The Forerunner library is a fast, lightweight, and extensible networking library created to aid in the development of robust network centric applications such as: IP Scanners, Port Knockers, Clients, Servers, etc. In it's current state, the Forerunner library is able to both synchronously and asynchronously scan and port knock IP addresses in order to obtain information about the device located at that endpoint such as: whether the IP is online, the physical MAC address, and etc. The library is a completely object oriented and event based library meaning that scan data is contained within specially crafted "scan" objects which are designed to handle all data from results to exceptions.

### Requirements
  - .NET Framework 4.6.1

# Features
| Method              | Description          | Usage                      |
| ------------------ | -------------------- | ---------------------- |
| Scan               | Scan a single IP for information     | ```Scan("192.168.1.1");```
| ScanRange          | Scan a range of IPs for information  | ```ScanRange("192.168.1.1", "192.168.1.255")```
| ScanList           | Scan a list of IPs for information  | ```ScanList("192.168.1.1, 192.168.1.2, 192.168.1.3")```
| PortKnock          | Ping every port of a single IP | ```PortKnock("192.168.1.1");```
| PortKnockRange     | Ping every port in a range of IPs | ```PortKnockRange("192.168.1.1", "192.168.1.255");```
| PortKnockList      | Ping every port using a list of IPs | ```PortKnockList("192.198.1.1, 192.168.1.2, 192.168.1.3");```
| IsHostAlive        | Ping a host N times for X milliseconds | ```IsHostAlive("192.168.1.1", 5, 1000);```
| GetAveragePingResponse | Get average ping response for a host | ```GetAveragePingResponse("192.168.1.1", 5, 1000);```
| IsPortOpen         | Ping individual ports via TCP & UDP | ```IsPortOpen("192.168.1.1", 45000, new TimeSpan(1000), false);```

# Examples
### IP Scanning

Scanning a network is a commonplace task in this digital age and so I have taken the liberty to make this as simple as possible for any future programmer whom may wish to do such a thing in an easy way. The Forerunner library is completely object oriented, thus making it ideal for plug and play situations; the object for IP scanning is called an <i>IPScanObject</i> and it actually contains quite a few properties:

- Address (<i><b>String</b></i>)
- IP (<i><b>IPAddress</b></i>)
- Ping (<i><b>Long</b></i>)
- Hostname (<i><b>String</b></i>)
- MAC (<i><b>String</b></i>)
- isOnline (<i><b>Bool</b></i>)
- Errors (<i><b>Exception</b></i>)

With the object in mind, let's try and create a new object and perform a scan using it. There are multiple ways to go about this, however, the simplest way to get started is to first create a new <i>Scanner</i> object so we can access our scanning methods. Next, create an <i>IPScanObject</i> and then set it to the <i>Scan</i> method with the IP you would like to enumerate; for example:

##### Synchronous

```c#
using System;
using Forerunner; // Remember to import our library.

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Our IP we would like to scan.
            string ip = "192.168.1.1";

            // Create a new scanner object.
            Scanner s = new Scanner();

            // Create a new scan object and perform a scan.
            IPScanObject result = s.Scan(ip);

            // Output that we have finished the scan.
            if (result.Errors != null)
                Console.WriteLine("[x] An error occurred during the scan.");
            else
                Console.WriteLine("[+] " + ip + " has been successfully scanned!")

            // Allow the user to exit at any time.
            Console.Read();
        }
    }
}
```

Another way, which is my preferred method of operation, is to create a <i>Scanner</i> object and subscribe to the <i>Event Handlers</i> of things like <i>ScanAsyncProgressChanged</i> or <i>ScanAsyncComplete</i>, that way I have full control over my async methods; I can control how they're progress states affect my application and so on; for example:

##### Asynchronous

```c#
using System;
using System.Threading.Tasks;
using Forerunner; // Remember to import our library.

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Our IP we would like to scan.
            string ip = "192.168.1.1";

            // Setup our scanner object.
            Scanner s = new Scanner();
            s.ScanAsyncProgressChanged += new ScanAsyncProgressChangedHandler(ScanAsyncProgressChanged);
            s.ScanAsyncComplete += new ScanAsyncCompleteHandler(ScanAsyncComplete);

            // Start a new scan task with our ip.
            TaskFactory task = new TaskFactory();
            task.StartNew(() => s.ScanAsync(ip));

            // Allow the user to exit at any time.
            Console.Read();
        }

        static void ScanAsyncProgressChanged(object sender, ScanAsyncProgressChangedEventArgs e)
        {
            // Do something here with e.Progress, or you could leave this event
            // unsubscribed so you wouldn't have to do anything.
        }

        static void ScanAsyncComplete(object sender, ScanAsyncCompleteEventArgs e)
        {
           // Do something with the IPScanObject aka e.Result.
            if (e.Result.Errors != null)
                Console.WriteLine("[x] An error occurred during the scan.");
            else
                Console.WriteLine("[+] " + e.Result.IP + " has been successfully scanned!")
        }
    }
}
```

### Port Knocking
I know what you're thinking. Port knocking? Yes, and no. The term doesn't mean port knocking in the traditional sense of connecting through a predefined set of ports, but rather just checking if any ports are actually open. It's literally "knocking" on a port in every sense of the word by trying to connect to each port and sending data. Just like with IP scanning, port knocking uses a custom object which is called a "<i>Port Knock Scan Object</i>" or <i>PKScanObject</i> for short. The <i>PKScanObject</i> actually contains a list of <i>PKServiceObject</i>s which in turn hold our port data; the service object contains the following properties:

- IP (<i><b>String</b></i>)
- Port (<i><b>Int</b></i>)
- Protocol (<i><b>PortType</b></i>)
- Status (<i><b>Bool</b></i>)

Port knocking is in similar fashion with IP scanning. First, create a <i>Scanner</i> object. Next, create a new <i>PKScanObject</i> and set it to the <i>PortKnock</i> method with the IP of your choosing, then display your results; for example:

##### Synchronous

```c#
using System;
using Forerunner; // Remember to import our library.

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Our IP we would like to scan.
            string ip = "192.168.1.1";

            // Create a new scanner object.
            Scanner s = new Scanner();

            // Create a new scan object and perform a scan.
            PKScanObject result = s.PortKnock(ip);

            // Output that we have finished the scan.
            if (result.Errors != null)
                Console.WriteLine("[x] An error occurred during the scan.");
            else
                Console.WriteLine("[+] " + ip + " has been successfully scanned!")

           // Display our results.
           foreach (PKServiceObject port in result.Services)
           {
                Console.WriteLine("[+] IP: " + port.IP + " | " +
                                  "Port: " + port.Port.ToString() + " | " +
                                  "Protocol: " + port.Protocol.ToString() + " | " +
                                  "Status: " + port.Status.ToString());
           }

           // Allow the user to exit at any time.
           Console.Read();
        }
    }
}
```

Lastly, I will show you a simple example of port knocking asynchronously. It is essentially the same as port knocking synchronously except for the fact that you can use events to your advantage. You can get progress updates without having to worry about UIs crashing or systems being in a locked state; for example:

##### Asynchronous

```c#
using System;
using System.Threading.Tasks;
using Forerunner; // Remember to import our library.

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Our IP we would like to scan.
            string ip = "192.168.1.1";

            // Setup our scanner object.
            Scanner s = new Scanner();
            s.PortKnockAsyncProgressChanged += new PortKnockAsyncProgressChangedHandler(PortKnockAsyncProgressChanged);
            s.PortKnockAsyncComplete += new PortKnockAsyncCompleteHandler(PortKnockAsyncComplete);

            // Start a new scan task with our ip.
            TaskFactory task = new TaskFactory();
            task.StartNew(() => s.PortKnockAsync(ip));

            // Allow the user to exit at any time.
            Console.Read();
        }

        static void PortKnockAsyncProgressChanged(object sender, PortKnockAsyncProgressChangedEventArgs e)
        {
            // Display our progress so we know the ETA.
            if (e.Progress == 99)
            {
                Console.Write(e.Progress.ToString() + "%...");
                Console.WriteLine("100%!");
            }
            else
                Console.Write(e.Progress.ToString() + "%...");
        }

        static void PortKnockAsyncComplete(object sender, PortKnockAsyncCompleteEventArgs e)
        {
            // Tell the user that the port knock was complete.
            Console.WriteLine("[+] Port Knock Complete!");

            // Check if we resolved an error.
            if (e.Result == null)
                Console.WriteLine("[X] The port knock did not return any data!");
            else
            {
                // Check if we have any ports recorded.
                if (e.Result.Services.Count == 0)
                    Console.WriteLine("[!] No ports were open during the knock.");
                else
                {
                    // Display our ports and their details.
                    foreach (PKServiceObject port in e.Result.Services)
                    {
                        Console.WriteLine("[+] IP: " + port.IP + " | " +
                                          "Port: " + port.Port.ToString() + " | " +
                                          "Protocol: " + port.Protocol.ToString() + " | " +
                                          "Status: " + port.Status.ToString());
                    }
                }
            }
        }
    }
}
```

# Credits
**Icon:** `monkik` <br>
https://www.flaticon.com/authors/monkik

# License

Copyright © ∞ Jason Drawdy (CloneMerge)

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
