using System;
using System.Linq;

namespace ANN_Experiment
{
    internal class Neuron
    {

        private readonly Random random = new Random();

        private readonly Func<double, double> activationFunction;

        private readonly int numberOfInputs;

        // w_0 to w_n  where w_bias is the last 
        public double[] Weights { get; set; }

        public double Bias { get; set; }

        public Neuron(Func<double, double> activationFunction, int numberOfInputs)
        {
            this.activationFunction = activationFunction;
            this.numberOfInputs = numberOfInputs;

            Bias = Randoms.RandomDouble(1, -1);
            Weights = new double[numberOfInputs];
            for (int i = 0; i < Weights.Length; i++)
                Weights[i] = Randoms.RandomDouble(1, -1);
        }

        public double Output(double[] inputs)
        {
            if (inputs.Length != numberOfInputs)
                return double.NaN;

            double net = Bias; 
            for (int i = 0; i < inputs.Length; i++)
                net += inputs[i]*Weights[i];

            return activationFunction(net);
        }

        public Neuron Mutate()
        {
            Neuron mutatedNeuron = new Neuron(activationFunction, numberOfInputs);
            if (Randoms.RandomBool(GlobalVars.NeuronValueMutationProbailiy))
            {
                mutatedNeuron.Bias += -GlobalVars.MaxMutation + random.NextDouble() * (GlobalVars.MaxMutation *2);
            }

            for (int i = 0; i < Weights.Length; i++)
                if (Randoms.RandomBool(GlobalVars.NeuronValueMutationProbailiy))
                {
                    double diff = -GlobalVars.MaxMutation + random.NextDouble() * (GlobalVars.MaxMutation * 2);
                    mutatedNeuron.Weights[i] += diff; 
                }
            return mutatedNeuron;
        }

        public override string ToString()
        {
            return $"Bias: {Bias} Weights: {Weights[0]}, {Weights[1]}";
        }
    }
}