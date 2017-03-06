using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ANN_Experiment
{
    internal class Program
    {
        private static List<Network> population;

        private static void Main(string[] args)
        {
            InitializePopulation(GlobalVars.PopulationSize);

            var inputBools = new List<bool[]>
            {
                new[] {false, false},
                new[] {true, false},
                new[] {false, true},
                new[] {true, true}
            };


            StreamWriter outWriter = new StreamWriter("log.csv");
            DateTime Started = DateTime.Now;
            for (int generation = 0; generation < GlobalVars.Generations; generation++)
            {
                if (generation%500 == 0)
                {
                    Console.Out.WriteLine("Starting selection for genereation {0}, ETA: {1} minutes", generation, (DateTime.Now - Started).TotalMinutes / generation  * (GlobalVars.Generations - generation));
                    outWriter.Write(generation);
                    foreach (var network in population)
                    {
                        outWriter.Write(",{0}", network.Fitness(inputBools));
                    }
                    outWriter.WriteLine();
                    outWriter.Flush();
                }

                List<Network> newGeneration = new List<Network>();
                foreach (var network in population)
                {
                    Network newNetwork = network.Mutate();
                    double newFitness = newNetwork.Fitness(inputBools);
                    double oldFitness = network.Fitness(inputBools);
                    newGeneration.Add(newFitness > oldFitness ? newNetwork : network);
                }                
                population = newGeneration;

            }
            
            foreach (Network network in population)
            {
                Console.Out.WriteLine("Fitness: {0}", network.Fitness(inputBools));
            }

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