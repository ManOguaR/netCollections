//*************************************************************************************
// File:     DefaultSwap.cs
//*************************************************************************************
// Description: Encapsula un intercambiador simple.
//*************************************************************************************
// Classes:      DefaultSwap : ISwap
//*************************************************************************************
// Author:      http://www.codeproject.com/csharp/csquicksort.asp
//*************************************************************************************

using netCollections;
using System.Collections;

namespace System.Utils.Sorting
{
    /// <summary>
    /// Representa un intercambiador simple.
    /// </summary>
    public class DefaultSwap : ISwap
    {
        public void Swap(IList array, int left, int right)
        {
            (array[right], array[left]) = (array[left], array[right]);
        }

        public void Set(IList array, int left, int right)
        {
            array[left] = array[right];
        }

        public void Set(IList array, int left, object obj)
        {
            array[left] = obj;
        }
    }
}
