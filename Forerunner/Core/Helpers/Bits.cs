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

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

#endregion
namespace Forerunner
{
    public static class Bits
    {
        public static byte[] Not(byte[] bytes)
        {
            return bytes.Select(b => (byte)~b).ToArray();
        }

        public static byte[] And(byte[] A, byte[] B)
        {
            return A.Zip(B, (a, b) => (byte)(a & b)).ToArray();
        }

        public static byte[] Or(byte[] A, byte[] B)
        {
            return A.Zip(B, (a, b) => (byte)(a | b)).ToArray();
        }

        public static bool GE(byte[] A, byte[] B)
        {
            return A.Zip(B, (a, b) => a == b ? 0 : a < b ? 1 : -1)
                .SkipWhile(c => c == 0)
                .FirstOrDefault() >= 0;
        }

        public static bool LE(byte[] A, byte[] B)
        {
            return A.Zip(B, (a, b) => a == b ? 0 : a < b ? 1 : -1)
                .SkipWhile(c => c == 0)
                .FirstOrDefault() <= 0;
        }

        public static byte[] GetBitMask(int sizeOfBuff, int bitLen)
        {
            var maskBytes = new byte[sizeOfBuff];
            var bytesLen = bitLen / 8;
            var bitsLen = bitLen % 8;
            for (var i = 0; i < bytesLen; i++)
            {
                maskBytes[i] = 0xff;
            }
            if (bitsLen > 0) maskBytes[bytesLen] = (byte)~Enumerable.Range(1, 8 - bitsLen).Select(n => 1 << n - 1).Aggregate((a, b) => a | b);
            return maskBytes;
        }

        /// <summary>
        /// Counts the number of leading 1's in a bitmask.
        /// Returns null if value is invalid as a bitmask.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int? GetBitMaskLength(byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));

            var bitLength = 0;
            var idx = 0;

            // find beginning 0xFF
            for (; idx < bytes.Length && bytes[idx] == 0xff; idx++);
            bitLength = 8*idx;

            if (idx < bytes.Length)
            {
                switch (bytes[idx])
                {
                    case 0xFE: bitLength += 7; break;
                    case 0xFC: bitLength += 6; break;
                    case 0xF8: bitLength += 5; break;
                    case 0xF0: bitLength += 4; break;
                    case 0xE0: bitLength += 3; break;
                    case 0xC0: bitLength += 2; break;
                    case 0x80: bitLength += 1; break;
                    case 0x00: break;
                    default: // invalid bitmask
                        return null;
                }
                // remainder must be 0x00
                if (bytes.Skip(idx + 1).Any(x => x != 0x00)) return null;
            }
            return bitLength;
        }


        public static byte[] Increment(byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));

            var incrementIndex = Array.FindLastIndex(bytes, x => x < byte.MaxValue);
            if (incrementIndex < 0) throw new OverflowException();
            return bytes
                .Take(incrementIndex)
                .Concat(new byte[] { (byte)(bytes[incrementIndex] + 1) })
                .Concat(new byte[bytes.Length - incrementIndex - 1])
                .ToArray();
        }

    }
}
