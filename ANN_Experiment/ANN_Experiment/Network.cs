using System;
using System.Collections.Generic;
using System.Linq;

namespace ANN_Experiment
{
    internal class Network
    {
        private readonly Random random = new Random();


        public Network(int numberOfInputValues, int[] hiddenLayerDefinitions)
        {
            NumberOfInputValues = numberOfInputValues;
            HiddenLayerDefinitions = hiddenLayerDefinitions;
            HiddenLayers = new List<Neuron[]>();
            Func<double, double> sigmoidFunc = x => 2.0/(1.0 + Math.Exp(-2.0*x)) - 1.0;

            for (int i = 0; (hiddenLayerDefinitions != null) && (i < hiddenLayerDefinitions.Length); i++)
            {
                var layer = new Neuron[hiddenLayerDefinitions[i]];
                for (int j = 0; j < hiddenLayerDefinitions[i]; j++)
                {
                    int numberOfPreviousLayer = i == 0 ? numberOfInputValues : hiddenLayerDefinitions[i - 1];
                    layer[j] = new Neuron(sigmoidFunc, numberOfPreviousLayer);
                }
                HiddenLayers.Add(layer);
            }

            OutputNeuron = new Neuron(sigmoidFunc, HiddenLayers.Last().Length);
        }

        public double LastFitnessValue { get; set; }

        public List<Neuron[]> HiddenLayers { get; }

        public Neuron OutputNeuron { get; set; }

        public int[] HiddenLayerDefinitions { get; }

        public int NumberOfInputValues { get; }

        public double GetValue(bool[] input)
        {
            // transform the input bools into doubles 
            var inputDoubles = new double[input.Length];
            for (int i = 0; i < inputDoubles.Length; i++)
                inputDoubles[i] = input[i] ? 1.0 : 0.0;

            // Calculate each layer 
            foreach (var layer in HiddenLayers)
            {
                var outputs = new double[layer.Length];
                for (int i = 0; i < layer.Length; i++)
                    outputs[i] = layer[i].Output(inputDoubles);
                inputDoubles = outputs;
            }

            // Return the output from the output layer
            return OutputNeuron.Output(inputDoubles);
        }

        public double Fitness(List<bool[]> inputValues)
        {
            double result = 0.0;
            foreach (var input in inputValues)
            {
                bool xor = input.Where(x => x).Count() == 1;
                double annResult = GetValue(input);
                result += 1 - 0 - Math.Abs((xor ? 1 : 0) - annResult);
            }
            result = result/4.0;
            LastFitnessValue = result;
            return result;
        }

        public Network Mutate()
        {
            Network mutatedNetwork = new Network(NumberOfInputValues, HiddenLayerDefinitions);
            // Add the mutated layers 
            for (int layerNumber = 0; layerNumber < HiddenLayers.Count; layerNumber++)
                for (int neuronNumber = 0; neuronNumber < HiddenLayers[layerNumber].Length; neuronNumber++)
                {
                    Neuron neuron = HiddenLayers[layerNumber][neuronNumber];
                    mutatedNetwork.HiddenLayers[layerNumber][neuronNumber] = random.NextDouble() <
                                                                             GlobalVars.NeuronMutationProbailiy
                        ? neuron.Mutate()
                        : neuron;
                }

            if (random.NextDouble() < GlobalVars.NeuronMutationProbailiy)
                mutatedNetwork.OutputNeuron = OutputNeuron.Mutate();

            return mutatedNetwork;
        }

        public override string ToString()
        {
            string hiddenNeurons = "";
            for (int index = 0; index < HiddenLayers.Count; index++)
            {
                var layer = HiddenLayers[index];
                hiddenNeurons += $"--- Hidden Layer {index} ---\n";
                for (int i = 0; i < layer.Length; i++)
                {
                    Neuron neuron = layer[i];
                    hiddenNeurons += $"Neuron {i}: {neuron}\n";
                }
            }

            hiddenNeurons += $"Output {OutputNeuron} ";
            hiddenNeurons += "\n-----------------------\n";
            return hiddenNeurons;
        }
    }
}