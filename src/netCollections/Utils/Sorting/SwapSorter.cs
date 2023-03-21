//*************************************************************************************
// File:     SwapSorter.cs
//*************************************************************************************
// Description: Encapsula una estructura para la reordenacion por intercambio de un
//              objeto derivado de netCollections.IList
//*************************************************************************************
// Classes:      SwapSorter : ISorter
//*************************************************************************************
// Author:      http://www.codeproject.com/csharp/csquicksort.asp
//*************************************************************************************

using System.Collections;

namespace System.Utils.Sorting
{
    /// <summary>
    /// Representa una estructura para la reordenacion por intercambio de un objeto derivado de netCollections.IList
    /// </summary>
    public abstract class SwapSorter : ISorter
    {
        private IComparer comparer;
        private ISwap swapper;

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public SwapSorter()
        {
            this.comparer = new ComparableComparer();
            this.swapper = new DefaultSwap();
        }
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="comparer">El comparador a emplear.</param>
        /// <param name="swapper">El intercambiador a emplear.</param>
        public SwapSorter(IComparer comparer, ISwap swapper)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            this.swapper = swapper ?? throw new ArgumentNullException(nameof(swapper));
        }
        /// <summary>
        /// Devuelve o establece el comparador.
        /// </summary>
        public IComparer Comparer
        {
            get
            {
                return this.comparer;
            }
            set
            {
                this.comparer = value ?? throw new ArgumentNullException("comparer");
            }
        }
        /// <summary>
        /// Devuelve o establece el intercambiador.
        /// </summary>
        public ISwap Swapper
        {
            get
            {
                return this.swapper;
            }
            set
            {
                this.swapper = value ?? throw new ArgumentNullException("swapper");
            }
        }

        /// <summary>
        /// Ordena los valores contenidos en <see cref="list"/> segun los criterios proporcionados por comparador e intercambiador.
        /// </summary>
        /// <param name="list">Un objeto derivado de netCollections.IList que contiene los valores a ordenar.</param>
		public abstract void Sort(IList list);
    }
}
