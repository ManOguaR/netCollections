//*************************************************************************************
// File:     ISorter.cs
//*************************************************************************************
// Description: Interface que define metodos que representan un reordenador.
//*************************************************************************************
// Interfaces:  ISorter
//*************************************************************************************
// Author:      http://www.codeproject.com/csharp/csquicksort.asp
//*************************************************************************************

using System.Collections;

namespace netCollections.Sorting
{
    public interface ISorter
    {
        void Sort(IList list);
    }
}
