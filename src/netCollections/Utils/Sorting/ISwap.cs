//*************************************************************************************
// File:     ISwap.cs
//*************************************************************************************
// Description: Interface que define metodos que representan un intercambiador.
//*************************************************************************************
// Interfaces:  ISwap
//*************************************************************************************
// Author:      http://www.codeproject.com/csharp/csquicksort.asp
//*************************************************************************************

using System.Collections;

namespace System.Utils.Sorting
{
    /// <summary>
    /// Interface que define metodos para la representacion de un intercambiador.
    /// </summary>
    public interface ISwap
    {
        void Swap(IList array, int left, int right);
        void Set(IList array, int left, int right);
        void Set(IList array, int left, object obj);
    }
}
