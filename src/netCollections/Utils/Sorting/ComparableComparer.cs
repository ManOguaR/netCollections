//*************************************************************************************
// File:     ComparableComparer.cs
//*************************************************************************************
// Description: Encapsula un comparador comparable.
//*************************************************************************************
// Classes:      ComparableComparer : IComparer
//*************************************************************************************
// Author:      http://www.codeproject.com/csharp/csquicksort.asp
//*************************************************************************************

using netCollections;
using System.Collections;

namespace System.Utils.Sorting
{
    /// <summary>
    /// Clase que representa un comparador comparable.
    /// </summary>
	public class ComparableComparer : IComparer
    {
        public int Compare(IComparable x, Object y)
        {
            return x.CompareTo(y);
        }

        #region IComparer Members
        int IComparer.Compare(Object x, Object y)
        {
            return Compare((IComparable)x, y);
        }
        #endregion
    }
}
