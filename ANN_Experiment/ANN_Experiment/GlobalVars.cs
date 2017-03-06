namespace ANN_Experiment
{
    internal class GlobalVars
    {
        public static double MaxMutation { get; } = 2;

        public static double NeuronMutationProbailiy { get; } = 0.5;

        public static double Elites { get; } = 0.0;

        public static int PopulationSize { get; } = 10;

        public static int Generations { get; } = 3000;

        public static double NeuronValueMutationProbailiy { get; set; } = 0.9;
    }
}