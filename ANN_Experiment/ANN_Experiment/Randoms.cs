using System;

namespace ANN_Experiment
{
    internal class Randoms
    {
        private static readonly Random Random = new Random();

        public static double RandomDouble(double max, double min = 0)
        {
            return min + Random.NextDouble()*(max - min);
        }


        public static bool RandomBool(double probability = 0.4)
        {
            return Random.NextDouble() <= probability;
        }
    }
}