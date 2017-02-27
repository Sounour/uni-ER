using System;
using System.Collections.Generic;
using System.Linq;

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


            for (int generation = 0; generation < GlobalVars.Generations; generation++)
            {
                Console.Out.WriteLine("Starting selection for genereation {0}", generation);

                SortedList<double, Network> ranking = new SortedList<Double,Network>(); 
                foreach (Network instance in population)
                {
                    ranking.Add(instance.Fitness(inputBools),instance);
                }

                List<Network> newGeneration = new List<Network>();
                int numberOfElites = (int) Math.Round(GlobalVars.Generations*GlobalVars.Elites); 
                newGeneration.AddRange(ranking.Values.Take(numberOfElites));

                foreach (var nonSelectedNetwork in ranking.Skip(numberOfElites))
                {
                    newGeneration.Add(nonSelectedNetwork.Mutate());

                }

                population = newGeneration;

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