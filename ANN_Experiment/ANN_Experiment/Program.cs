using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ANN_Experiment
{
    internal class Program
    {
        private static List<Network> population;
        private static List<bool[]> inputBools;

        private static void Main(string[] args)
        {
            InitializePopulation(GlobalVars.PopulationSize);

            inputBools = new List<bool[]>
            {
                new[] {false, false},
                new[] {true, false},
                new[] {false, true},
                new[] {true, true}
            };

            perfectXor();

             mutateAndEvolve();
        }

        public static void perfectXor()
        {
            Network n = new Network(2, new[] {2});
            n.HiddenLayers[0][0].Bias = 0.0; 
            n.HiddenLayers[0][0].Weights = new[] {1.0, -1.0 };

            n.HiddenLayers[0][1].Bias = 0.0;
            n.HiddenLayers[0][1].Weights = new[] {1.0, -1.0};

            n.OutputNeuron.Bias = 0.0; 
            n.OutputNeuron.Weights = new[] { 1.0 , 1.0};

            Console.Out.WriteLine(n.HiddenLayers[0][1].Output(new []{ 0.0, 0.0}));
            Console.Out.WriteLine(n.HiddenLayers[0][1].Output(new[] { 1.0, 0.0 }));
            Console.Out.WriteLine(n.HiddenLayers[0][1].Output(new[] { 0.0, 1.0 }));
            Console.Out.WriteLine(n.HiddenLayers[0][1].Output(new[] { 1.0, 1.0 }));



            Console.Out.WriteLine("-------");

            Console.Out.WriteLine(n.Fitness(inputBools));

            Console.Out.WriteLine("-------"); 
            foreach (var input in inputBools)
                Console.Out.WriteLine(n.GetValue(input));

            Console.ReadLine();

        }

        public static void mutateAndEvolve()
        {
            StreamWriter outWriter = new StreamWriter("log.csv");
            DateTime Started = DateTime.Now;

            foreach (var network in population)
            {
                network.Fitness(inputBools); 
            }

            for (int generation = 0; generation < GlobalVars.Generations; generation++)
            {
                if (generation%(GlobalVars.Generations /100) == 0)
                {
                    Console.Out.WriteLine("Starting selection for genereation {0}, ETA: {1} minutes", generation,
                        (DateTime.Now - Started).TotalMinutes / generation*(GlobalVars.Generations - generation));
                    outWriter.Write(generation);
                    foreach (Network network in population)
                        outWriter.Write(" {0}", network.Fitness(inputBools));
                    outWriter.WriteLine();
                    outWriter.Flush();
                }

                foreach (var network in population)
                {
                    if (network.LastFitnessValue > 0.99)
                    {
                        Console.Out.WriteLine(network);
                        Console.ReadLine();
                        return; 
                    }
                }
                
                var newGeneration = new List<Network>();
                Parallel.ForEach(population, network =>
                {
                    Network newNetwork = network.Mutate();
                    newNetwork.Fitness(inputBools);

                    int max = 0;
                    while (newNetwork.LastFitnessValue < network.LastFitnessValue && max < 1000)
                    {
                        max++; 
                        newNetwork =  network.Mutate();
                        newNetwork.Fitness(inputBools);
                    }

                    if (newNetwork.LastFitnessValue > network.LastFitnessValue)
                    {
                        Console.Out.WriteLine("{0} -> {1}", network.LastFitnessValue, newNetwork.LastFitnessValue);
                        //  Console.Out.WriteLine(newNetwork.ToString());
                    }

                    lock (newGeneration)
                    {
                        newGeneration.Add(newNetwork.LastFitnessValue > network.LastFitnessValue ? newNetwork : network);
                    }

                }); 

                population = newGeneration;
            }

            foreach (Network network in population)
                Console.Out.WriteLine("Fitness: {0}", network.Fitness(inputBools));

            Console.ReadLine();
        }

        private static void InitializePopulation(int i)
        {
            population = new List<Network>();
            for (int j = 0; j < i; j++)
                population.Add(new Network(2, new[] {2}));
        }
    }
}