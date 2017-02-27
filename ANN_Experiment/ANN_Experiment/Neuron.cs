using System;
using System.Linq;

namespace ANN_Experiment
{
    internal class Neuron
    {
        private readonly Func<double, double> activationFunction;

        // w_0 to w_n with possible additional w_bias at the end 
        private readonly double[] weights;


        public Neuron(Func<double, double> activationFunction, int numberOfInputs)
        {
            this.activationFunction = activationFunction;

            weights = new double[numberOfInputs];
            for (int i = 0; i < numberOfInputs; i++)
                weights[i] = (Program.Random.NextDouble() - 0.5)*2.0;
        }

        public double Output(double[] inputs)
        {
            if ((inputs.Length > weights.Length) || (inputs.Length + 1 < weights.Length))
                return double.NaN;

            double net = inputs.Length == weights.Length ? 0 : weights.Last();
            for (int i = 0; i < inputs.Length; i++)
                net += inputs[i]*weights[i];

            return activationFunction(net);
        }

        public void mutate()
        {
            for (int i = 0; i < weights.Length; i++)
            {
            }
        }
    }
}