//*************************************************************************************
// File:     ISorter.cs
//*************************************************************************************
// Description: Interface que define metodos que representan un reordenador.
//*************************************************************************************
// Interfaces:  ISorter
//*************************************************************************************
// Author:      http://www.codeproject.com/csharp/csquicksort.asp
//*************************************************************************************

using netCollections;
using System.Collections;

namespace System.Utils.Sorting
{
    public interface ISorter
    {
        void Sort(IList list);
    }
}
