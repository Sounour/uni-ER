using System;
using System.Linq;

namespace ANN_Experiment
{
    internal class Neuron
    {
        private readonly Func<double, double> activationFunction;

        private readonly int numberOfInputs;

        // w_0 to w_n  where w_bias is the last 
        private readonly double[] weights;


        public Neuron(Func<double, double> activationFunction, int numberOfInputs)
        {
            this.activationFunction = activationFunction;
            this.numberOfInputs = numberOfInputs;

            weights = new double[numberOfInputs + 1];
            for (int i = 0; i < weights.Length; i++)
                weights[i] = Randoms.RandomDouble(1, -1);
        }

        public double Output(double[] inputs)
        {
            if (inputs.Length != numberOfInputs)
                return double.NaN;

            double net = weights.Last();
            for (int i = 0; i < inputs.Length; i++)
                net += inputs[i]*weights[i];

            return activationFunction(net);
        }

        public Neuron Mutate()
        {
            Neuron mutatedNeuron = new Neuron(activationFunction, numberOfInputs);

            for (int i = 0; i < weights.Length; i++)
                if (Randoms.RandomBool(GlobalVars.MutationProbailiy))
                    mutatedNeuron.weights[i] += Randoms.RandomDouble(GlobalVars.MaxMutation);

            return mutatedNeuron;
        }
    }
}