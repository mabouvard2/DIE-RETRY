using System;
using System.Collections.Generic;
using System.Text;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Classe static afin de pouvoir utiliser rapidement le random partout
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// Attribut Random
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// Méthode permettant d'obtenir un float aléatoire
        /// </summary>
        /// <returns>Retourne un float aléatoire</returns>
        public static float GetNextFloat()
        {
            return (float)Random.NextDouble();
        }

        /// <summary>
        /// Méthode permettant d'obtenir un entier aléatoire
        /// </summary>
        /// <returns>Retourne un entier aléatoire</returns>
        public static int GetNextInt()
        {
            return Random.Next();
        }

        /// <summary>
        /// Méthode permettant d'obtenir un entier aléatoire entre deux bornes
        /// </summary>
        /// <param name="min">Borne inférieure</param>
        /// <param name="max">Borne supérieure</param>
        /// <returns>un entier aléatoire</returns>
        public static int GetNextInt(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}
