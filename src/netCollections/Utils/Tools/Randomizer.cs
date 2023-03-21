//*************************************************************************************
// File:        RandomGenerators.cs
//*************************************************************************************
// Description: Contains various pseudorandom and simple genuine random number
//              generators.
//*************************************************************************************
// Classes:     SimpleRandomizer
//              Dice
//*************************************************************************************
// Enum:        Dice.Sides
//*************************************************************************************
// Use:         SimpleRandomizer represents a simple pseudorandom number generator
//              intended to use in any situation that not requires a true deterministic
//              luck factor.
//              Dice encapsulates a structure to generate similar results to any board
//              game dice rolls. Results are genuine random numbers.
//*************************************************************************************
// Author:      ManOguaR
//*************************************************************************************

namespace System.Utils.Tools
{
    /// <summary>
    /// Encapsulates a static pseudorandom number generator.
    /// </summary>
    public class Randomizer
    {
        #region Static class members
        private static Random _seedRandomizer = null;

        static Randomizer()
        {
            _seedRandomizer = new Random(1234567890);
        }

        /// <summary>
        /// Devuelve una instancia de la clase <see cref="System.Utils.Tools.Randomizer"/> inicializada
        /// con un numero aleatoreo.
        /// </summary>
        /// <returns>Una instancia de la clase <see cref="System.Utils.Tools.Randomizer"/></returns>
        public static Randomizer GetInstance()
        {
            return new Randomizer(_seedRandomizer.Next());
        }

        #endregion

        private Random _rnd;

        // Not Creatable.
        private Randomizer(int seed)
        {
            _rnd = new Random(seed);
        }

        /// <summary>
        /// Returns a float value between 0.0 y 1.0
        /// </summary>
        /// <returns>Value between 0.0 y 1.0</returns>
        public float GetFloat()
        {
            return (float)_rnd.NextDouble();
        }
        /// <summary>
        /// Returns a double value between 0.0 y 1.0
        /// </summary>
        /// <returns>Value between 0.0 y 1.0</returns>
        public double GetDouble()
        {
            return _rnd.NextDouble();
        }
        /// <summary>
        /// Returns a integer value.
        /// </summary>
        /// <returns>Integer value</returns>
        public int GetInt()
        {
            return _rnd.Next();
        }
        /// <summary>
        /// Returns a integer value between 0 and <see cref="maxValue"/> parameter.
        /// </summary>
        /// <param name="maxValue">A integer value that represents the max value for the random number.</param>
        /// <returns>Value between 0 and <see cref="maxValue"/></returns>
        public int GetInt(int maxValue)
        {
            return _rnd.Next(maxValue);
        }
        /// <summary>
        /// Returns a byte matrix with size set by <see cref="numBytes"/> parameter.
        /// </summary>
        /// <param name="numBytes">Byte matrix size.</param>
        /// <returns>A byte matrix with size set by <see cref="numBytes"/> parameter, filled with random values.</returns>
        public byte[] GetBytes(int numBytes)
        {
            byte[] buff = new byte[numBytes];
            _rnd.NextBytes(buff);
            return buff;
        }
    }
}