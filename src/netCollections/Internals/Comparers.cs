﻿namespace netCollections
{
    /// <summary>
    /// A collection of methods to create IComparer and IEqualityComparer instances in various ways.
    /// </summary>
    internal static class Comparers
    {
        /// <summary>
        /// Class to change an IEqualityComparer{TKey} to an IEqualityComparer{KeyValuePair{TKey, TValue}} 
        /// Only the keys are compared.
        /// </summary>
        [Serializable]
        class KeyValueEqualityComparer<TKey, TValue> : IEqualityComparer<KeyValuePair<TKey, TValue>>
        {
            private readonly IEqualityComparer<TKey> keyEqualityComparer;

            public KeyValueEqualityComparer(IEqualityComparer<TKey> keyEqualityComparer)
            { this.keyEqualityComparer = keyEqualityComparer; }

            public bool Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            { return keyEqualityComparer.Equals(x.Key, y.Key); }

            public int GetHashCode(KeyValuePair<TKey, TValue> obj)
            {
                return Util.GetHashCode(obj.Key, keyEqualityComparer);
            }

            public override bool Equals(object obj)
            {
                if (obj is KeyValueEqualityComparer<TKey, TValue>)
                    return Equals(keyEqualityComparer, ((KeyValueEqualityComparer<TKey, TValue>)obj).keyEqualityComparer);
                else
                    return false;
            }

            public override int GetHashCode()
            {
                return keyEqualityComparer.GetHashCode();
            }
        }

        /// <summary>
        /// Class to change an IComparer{TKey} to an IComparer{KeyValuePair{TKey, TValue}} 
        /// Only the keys are compared.
        /// </summary>
        [Serializable]
        class KeyValueComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
        {
            private readonly IComparer<TKey> keyComparer;

            public KeyValueComparer(IComparer<TKey> keyComparer)
            { this.keyComparer = keyComparer; }

            public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            { return keyComparer.Compare(x.Key, y.Key); }

            public override bool Equals(object obj)
            {
                if (obj is KeyValueComparer<TKey, TValue>)
                    return Equals(keyComparer, ((KeyValueComparer<TKey, TValue>)obj).keyComparer);
                else
                    return false;
            }

            public override int GetHashCode()
            {
                return keyComparer.GetHashCode();
            }
        }

        /// <summary>
        /// Class to change an IComparer{TKey} and IComparer{TValue} to an IComparer{KeyValuePair{TKey, TValue}} 
        /// Keys are compared, followed by values.
        /// </summary>
        [Serializable]
        class PairComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
        {
            private readonly IComparer<TKey> keyComparer;
            private readonly IComparer<TValue> valueComparer;

            public PairComparer(IComparer<TKey> keyComparer, IComparer<TValue> valueComparer)
            {
                this.keyComparer = keyComparer;
                this.valueComparer = valueComparer;
            }

            public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            {
                int keyCompare = keyComparer.Compare(x.Key, y.Key);

                if (keyCompare == 0)
                    return valueComparer.Compare(x.Value, y.Value);
                else
                    return keyCompare;
            }

            public override bool Equals(object obj)
            {
                if (obj is PairComparer<TKey, TValue>)
                    return Equals(keyComparer, ((PairComparer<TKey, TValue>)obj).keyComparer) &&
                        Equals(valueComparer, ((PairComparer<TKey, TValue>)obj).valueComparer);
                else
                    return false;
            }

            public override int GetHashCode()
            {
                return keyComparer.GetHashCode() ^ valueComparer.GetHashCode();
            }
        }

        /// <summary>
        /// Class to change an <see cref="Comparison{T}"/> to an <see cref="IComparer{T}"/>.
        /// </summary>
        [Serializable]
        class ComparisonComparer<T> : IComparer<T>
        {
            private readonly Comparison<T> comparison;

            public ComparisonComparer(Comparison<T> comparison) { this.comparison = comparison; }

            public int Compare(T x, T y) { return comparison(x!, y!); }

            public override bool Equals(object obj)
            {
                if (obj is ComparisonComparer<T> comparer)
                    return comparison.Equals(comparer.comparison);
                else
                    return false;
            }

            public override int GetHashCode()
            {
                return comparison.GetHashCode();
            }
        }

        /// <summary>
        /// Class to change an Comparison{TKey} to an IComparer{KeyValuePair{TKey, TValue}}.
        /// GetHashCode cannot be used on this class.
        /// </summary>
        [Serializable]
        class ComparisonKeyValueComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
        {
            private readonly Comparison<TKey> comparison;

            public ComparisonKeyValueComparer(Comparison<TKey> comparison)
            { this.comparison = comparison; }

            public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            { return comparison(x.Key, y.Key); }

            public override bool Equals(object obj)
            {
                if (obj is ComparisonKeyValueComparer<TKey, TValue>)
                    return comparison.Equals(((ComparisonKeyValueComparer<TKey, TValue>)obj).comparison);
                else
                    return false;
            }

            public override int GetHashCode()
            {
                return comparison.GetHashCode();
            }
        }


        /// <summary>
        /// Given an Comparison on a type, returns an IComparer on that type. 
        /// </summary>
        /// <typeparam name="T">T to compare.</typeparam>
        /// <param name="comparison">Comparison delegate on T</param>
        /// <returns>IComparer that uses the comparison.</returns>
        public static IComparer<T> ComparerFromComparison<T>(Comparison<T> comparison)
        {
            if (comparison == null)
                throw new ArgumentNullException(nameof(comparison));

            return new ComparisonComparer<T>(comparison);
        }

        /// <summary>
        /// Given an IComparer on TKey, returns an IComparer on
        /// key-value Pairs. 
        /// </summary>
        /// <typeparam name="TKey">TKey of the pairs</typeparam>
        /// <typeparam name="TValue">TValue of the apris</typeparam>
        /// <param name="keyComparer">IComparer on TKey</param>
        /// <returns>IComparer for comparing key-value pairs.</returns>
        public static IComparer<KeyValuePair<TKey, TValue>> ComparerKeyValueFromComparerKey<TKey, TValue>(IComparer<TKey> keyComparer)
        {
            if (keyComparer == null)
                throw new ArgumentNullException(nameof(keyComparer));

            return new KeyValueComparer<TKey, TValue>(keyComparer);
        }

        /// <summary>
        /// Given an IEqualityComparer on TKey, returns an IEqualityComparer on
        /// key-value Pairs. 
        /// </summary>
        /// <typeparam name="TKey">TKey of the pairs</typeparam>
        /// <typeparam name="TValue">TValue of the apris</typeparam>
        /// <param name="keyEqualityComparer">IComparer on TKey</param>
        /// <returns>IEqualityComparer for comparing key-value pairs.</returns>
        public static IEqualityComparer<KeyValuePair<TKey, TValue>> EqualityComparerKeyValueFromComparerKey<TKey, TValue>(IEqualityComparer<TKey> keyEqualityComparer)
        {
            if (keyEqualityComparer == null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return new KeyValueEqualityComparer<TKey, TValue>(keyEqualityComparer);
        }

        /// <summary>
        /// Given an IComparer on TKey and TValue, returns an IComparer on
        /// key-value Pairs of TKey and TValue, comparing first keys, then values. 
        /// </summary>
        /// <typeparam name="TKey">TKey of the pairs</typeparam>
        /// <typeparam name="TValue">TValue of the apris</typeparam>
        /// <param name="keyComparer">IComparer on TKey</param>
        /// <param name="valueComparer">IComparer on TValue</param>
        /// <returns>IComparer for comparing key-value pairs.</returns>
        public static IComparer<KeyValuePair<TKey, TValue>> ComparerPairFromKeyValueComparers<TKey, TValue>(IComparer<TKey> keyComparer, IComparer<TValue> valueComparer)
        {
            if (keyComparer == null)
                throw new ArgumentNullException(nameof(keyComparer));
            if (valueComparer == null)
                throw new ArgumentNullException(nameof(valueComparer));

            return new PairComparer<TKey, TValue>(keyComparer, valueComparer);
        }

        /// <summary>
        /// Given an Comparison on TKey, returns an IComparer on
        /// key-value Pairs. 
        /// </summary>
        /// <typeparam name="TKey">TKey of the pairs</typeparam>
        /// <typeparam name="TValue">TValue of the apris</typeparam>
        /// <param name="keyComparison">Comparison delegate on TKey</param>
        /// <returns>IComparer for comparing key-value pairs.</returns>
        public static IComparer<KeyValuePair<TKey, TValue>> ComparerKeyValueFromComparisonKey<TKey, TValue>(Comparison<TKey> keyComparison)
        {
            if (keyComparison == null)
                throw new ArgumentNullException(nameof(keyComparison));

            return new ComparisonKeyValueComparer<TKey, TValue>(keyComparison);
        }

        /// <summary>
        /// Given an element type, check that it implements <see cref="IComparable{T}"/> or IComparable, then returns
        /// a IComparer that can be used to compare elements of that type.
        /// </summary>
        /// <returns>The <see cref="IComparer{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">T does not implement <see cref="IComparable{T}"/>.</exception>
        public static IComparer<T> DefaultComparer<T>()
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)) ||
                typeof(System.IComparable).IsAssignableFrom(typeof(T)))
            {
                return Comparer<T>.Default;
            }
            else
            {
                throw new InvalidOperationException(string.Format(Strings.UncomparableType, typeof(T).FullName));
            }
        }

        /// <summary>
        /// Given an key and value type, check that TKey implements <see cref="IComparable{T}"/> or IComparable, then returns
        /// a IComparer that can be used to compare KeyValuePairs of those types.
        /// </summary>
        /// <returns>The IComparer{KeyValuePair{TKey, TValue}} instance.</returns>
        /// <exception cref="InvalidOperationException">TKey does not implement <see cref="IComparable{T}"/>.</exception>
        public static IComparer<KeyValuePair<TKey, TValue>> DefaultKeyValueComparer<TKey, TValue>()
        {
            IComparer<TKey> keyComparer = DefaultComparer<TKey>();
            return ComparerKeyValueFromComparerKey<TKey, TValue>(keyComparer);
        }
    }
}
