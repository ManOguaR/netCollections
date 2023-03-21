//*************************************************************************************
// File:     QuickSorter.cs
//*************************************************************************************
// Description: Representa una estructura para la reordenacion rapida del contenido de
//              un objeto derivado de netCollections.IList
//*************************************************************************************
// Classes:      QuickSorter : SwapSorter
//*************************************************************************************
// Use:         Se emplea el parametro IComparer del constructor para la obtener un
//              criterio de comparacion de objetos, mientras que el parametro ISwap es
//              empleado para la operacion de intercambio que reordena la lista.
//*************************************************************************************
// Author:      http://www.codeproject.com/csharp/csquicksort.asp
//*************************************************************************************

using System.Collections;

namespace System.Utils.Sorting
{
    /// <summary>
    /// Representa una estructura para la reordenacion rapida del contenido de un objeto derivado de netCollections.IList
    /// </summary>
	public class QuickSorter : SwapSorter
    {
        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public QuickSorter()
            : base() { }

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="comparer">El comparador a emplear.</param>
        /// <param name="swapper">El intercambiador a emplear.</param>
        public QuickSorter(IComparer comparer, ISwap swapper)
            : base(comparer, swapper) { }

        /// <summary>
        /// Reordena el contenido del parametro <see cref="array"/> segun los criterios de comparador e intercambiador.
        /// </summary>
        /// <param name="array">Un objeto derivado de netCollections.IList</param>
        public override void Sort(IList array)
        {
            Sort(array, 0, array.Count - 1);
        }
        /// <summary>
        /// Reordena un segmento del parametro <see cref="array"/>, delimitado por <see cref="lower"/> y <see cref="upper"/>, segun los criterios de comparador e intercambiador.
        /// </summary>
        /// <param name="array">Un objeto derivado de netCollections.IList</param>
        /// <param name="lower">Un valor entero que representa el indice inferior del segmento a ordenar.</param>
        /// <param name="upper">Un valor entero que representa el indice superior del segmento a ordenar.</param>
        public void Sort(IList array, int lower, int upper)
        {
            // Check for non-base case
            if (lower < upper)
            {
                // Split and sort partitions
                int split = Pivot(array, lower, upper);
                Sort(array, lower, split - 1);
                Sort(array, split + 1, upper);
            }
        }

        #region internal
        private int Pivot(IList array, int lower, int upper)
        {
            // Pivot with first element
            int left = lower + 1;
            object pivot = array[lower];
            int right = upper;

            // Partition array elements
            while (left <= right)
            {
                // Find item out of place
                while ((left <= right) && (Comparer.Compare(array[left], pivot) <= 0))
                {
                    ++left;
                }

                while ((left <= right) && (Comparer.Compare(array[right], pivot) > 0))
                {
                    --right;
                }

                // Swap values if necessary
                if (left < right)
                {
                    Swapper.Swap(array, left, right);
                    ++left;
                    --right;
                }
            }

            // Move pivot element
            Swapper.Swap(array, lower, right);
            return right;
        }
        #endregion
    }
}
