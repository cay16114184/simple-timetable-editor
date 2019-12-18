﻿using System.Collections.Generic;
using System.Linq;
using Unicorn.Writer.Interfaces;

namespace Unicorn.Writer.Primitives
{
    /// <summary>
    /// The class which represents a PDF array object.
    /// </summary>
    public class PdfArray : PdfSimpleObject
    {
        private readonly IPdfPrimitiveObject[] _val;

        /// <summary>
        /// Create a new array whose contents are the given primitives.
        /// </summary>
        /// <param name="contents"></param>
        public PdfArray(IEnumerable<IPdfPrimitiveObject> contents)
        {
            _val = contents.ToArray();
        }

        /// <summary>
        /// Indexer to the contents of the array.
        /// </summary>
        /// <param name="i">The index of the array member.</param>
        /// <returns>The array member at the given index.</returns>
        public IPdfPrimitiveObject this[int i] => _val[i];

        /// <summary>
        /// Length of the array.
        /// </summary>
        public int Length => _val.Length;

        /// <summary>
        /// Convert the contents of the array into an array of bytes.
        /// </summary>
        /// <returns>An array of bytes which represent this object, serialised.</returns>
        protected override byte[] FormatBytes()
        {
            List<byte> byteList = new List<byte>() { 0x5b };
            int runningCount = 1;
            foreach (IPdfPrimitiveObject obj in _val)
            {
                int objLength = obj.ByteLength;
                if (runningCount + objLength > 254)
                {
                    byteList.Add(0xa);
                    runningCount = 0;
                }
                obj.WriteTo(byteList);
                if (objLength > 254)
                {
                    byteList.Add(0xa);
                    runningCount = 0;
                }
                else
                {
                    runningCount += objLength;
                }
            }
            byteList.Add(0x5d);
            byteList.Add(0xa);
            return byteList.ToArray();
        }
    }
}
