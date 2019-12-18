﻿using System;
using System.Collections.Generic;

namespace Tests.Utility.Extensions
{
    public static class EnumerableExtensions
    {
        public static int MaxIndex<T>(this IEnumerable<T> sequence) where T : IComparable<T>
        {
            if (sequence == null)
            {
                throw new NullReferenceException();
            }

            int maxIndex = -1;
            T maxValue = default;

            int index = 0;
            foreach (T item in sequence)
            {
                if (item.CompareTo(maxValue) > 0 || maxIndex == -1)
                {
                    maxIndex = index;
                    maxValue = item;
                }
                index++;
            }
            return maxIndex;
        }
    }
}
