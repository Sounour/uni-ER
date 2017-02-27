using System;
using System.Collections.Generic;
using System.Linq;

namespace ANN_Experiment
{
    internal class Network
    {
        private readonly List<Neuron[]> hiddenLayers;
        private readonly Neuron outputNeuron;

        private int inputValues;
        private int[] hiddenNeurons;

        public Network(int inputValues, int[] hiddenNeurons = null)
        {
            this.inputValues = inputValues;
            this.hiddenNeurons = hiddenNeurons; 

            Func<double, double> sigmoidFunc = x => 2.0/(1.0 + Math.Exp(-2.0*x)) - 1.0;

            hiddenLayers = new List<Neuron[]>();
            if (hiddenNeurons != null)
                for (int i = 0; i < hiddenNeurons.Length; i++)
                {
                    var layer = new Neuron[hiddenNeurons[i]];
                    for (int j = 0; j < hiddenNeurons[i]; j++)
                        layer[j] = new Neuron(sigmoidFunc, i == 0 ? inputValues : hiddenNeurons[i - 1]);
                    hiddenLayers.Add(layer);
                }

            outputNeuron = new Neuron(sigmoidFunc, hiddenLayers.Last().Length);
        }

        public double GetValue(bool[] input)
        {
            // transform the input bools into doubles 
            var inputDoubles = new double[input.Length];
            for (int i = 0; i < inputDoubles.Length; i++)
                inputDoubles[i] = input[i] ? 1 : 0;

            // Calculate each layer 
            foreach (var layer in hiddenLayers)
            {
                var outputs = new double[layer.Length];
                for (int i = 0; i < layer.Length; i++)
                    outputs[i] = layer[i].Output(inputDoubles);
                inputDoubles = outputs;
            }

            // Return the output from the output layer
            return outputNeuron.Output(inputDoubles);
        }

        public double Fitness(List<bool[]> inputValues)
        {
            double result = 0;
            foreach (var input in inputValues)
                result += 1 - Math.Abs((input.Where(x => x).Count() == 1 ? 1 : 0) - GetValue(input));
            result = 1.0/4.0*result;
            return result;
        }

        public Network Mutate()
        {
            Network mutatedNetwork = new Network(inputValues,hiddenNeurons);
            foreach (Neuron[] layer in mutatedNetwork.hiddenLayers)
            {
                for (int i = 0; i < UPPER; i++)
                {
                    
                }
                
            }
            

        }
    }
}