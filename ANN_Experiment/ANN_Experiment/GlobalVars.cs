namespace ANN_Experiment
{
    internal class GlobalVars
    {
        public static double MaxMutation { get; } = 0.2;

        public static double NeuronMutationProbailiy { get; } = 0.3;

        public static double Elites { get; } = 0.0;

        public static int PopulationSize { get; } = 2000;

        public static int Generations { get; } = 100000;

        public static double NeuronValueMutationProbailiy { get; set; } = 0.3;
    }
}